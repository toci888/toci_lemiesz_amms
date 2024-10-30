import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, Button, FlatList, StyleSheet } from 'react-native';

const MedicationManagement = () => {
    const [medication, setMedication] = useState('');
    const [medications, setMedications] = useState([]);
    const [language, setLanguage] = useState('English');

    useEffect(() => {
        fetchMedications();
    }, []);

    const fetchMedications = async () => {
        try {
            const response = await fetch('http://192.168.45.47:7040/api/medications');
            const data = await response.json();
            setMedications(data);
        } catch (error) {
            console.error('Error fetching medications:', error);
        }
    };

    const addMedication = async () => {
        if (medication.trim()) {
            try {
                const response = await fetch('http://192.168.45.47:7040/api/medications', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({ name: medication }),
                });
                if (response.ok) {
                    setMedication('');
                    fetchMedications(); // Refresh medications after adding
                }
            } catch (error) {
                console.error('Error adding medication:', error);
            }
        }
    };

    const handleLanguageChange = (lang) => {
        setLanguage(lang);
    };

    return (
        <View style={styles.container}>
            <Text style={styles.header}>
                {language === 'English' ? 'Medication Management' : 'Zarządzanie lekami'}
            </Text>
            <FlatList
                data={medications}
                renderItem={({ item }) => <Text style={styles.medication}>{item.name}</Text>}
                keyExtractor={(item) => item.id.toString()}
                style={styles.medicationsList}
            />
            <TextInput
                style={styles.input}
                value={medication}
                onChangeText={setMedication}
                placeholder={language === 'English' ? 'Enter medication name...' : 'Wprowadź nazwę leku...'}
            />
            <Button title={language === 'English' ? 'Add Medication' : 'Dodaj lek'} onPress={addMedication} />
            <View style={styles.languageContainer}>
                <Button title="English" onPress={() => handleLanguageChange('English')} />
                <Button title="Polish" onPress={() => handleLanguageChange('Polish')} />
            </View>
        </View>
    );
};

// Similar styles as before

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 10,
        backgroundColor: '#fff',
    },
    header: {
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 10,
    },
    medicationsList: {
        flex: 1,
        marginBottom: 10,
    },
    medication: {
        padding: 10,
        borderRadius: 5,
        backgroundColor: '#e1f5fe',
        marginVertical: 5,
    },
    input: {
        borderWidth: 1,
        borderColor: '#ccc',
        borderRadius: 5,
        padding: 10,
        marginBottom: 10,
    },
    languageContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
});

export default MedicationManagement;