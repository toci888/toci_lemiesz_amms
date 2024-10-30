import React, { useState } from 'react';
import { View, Text, TextInput, Button, FlatList, StyleSheet } from 'react-native';

const MedicationManagement = () => {
    const [medication, setMedication] = useState('');
    const [medications, setMedications] = useState([]);
    const [language, setLanguage] = useState('English'); // Default language

    const addMedication = () => {
        if (medication.trim()) {
            setMedications(prevMedications => [...prevMedications, { id: Date.now().toString(), name: medication }]);
            setMedication('');
        }
    };

    const handleLanguageChange = (lang) => {
        setLanguage(lang);
    };

    return (
        <View style={styles.container}>
            <Text style={styles.header}>{language === 'English' ? 'Medication Management' : 'Zarządzanie lekami'}</Text>
            <FlatList
                data={medications}
                renderItem={({ item }) => <Text style={styles.medication}>{item.name}</Text>}
                keyExtractor={item => item.id}
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