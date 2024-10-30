import React, { useState } from 'react';
import { View, Text, TextInput, Button, StyleSheet } from 'react-native';

const PatientProfile = () => {
    const [name, setName] = useState('');
    const [age, setAge] = useState('');
    const [language, setLanguage] = useState('English'); // Default language

    const handleLanguageChange = (lang) => {
        setLanguage(lang);
    };

    const saveProfile = () => {
        // Here you would typically handle saving the profile data
        alert(`Profile saved:\nName: ${name}\nAge: ${age}`);
    };

    return (
        <View style={styles.container}>
            <Text style={styles.header}>
                {language === 'English' ? 'Patient Profile' : 'Profil pacjenta'}
            </Text>
            <TextInput
                style={styles.input}
                value={name}
                onChangeText={setName}
                placeholder={language === 'English' ? 'Enter name...' : 'Wprowadź imię...'}
            />
            <TextInput
                style={styles.input}
                value={age}
                onChangeText={setAge}
                placeholder={language === 'English' ? 'Enter age...' : 'Wprowadź wiek...'}
                keyboardType="numeric"
            />
            <Button title={language === 'English' ? 'Save Profile' : 'Zapisz profil'} onPress={saveProfile} />
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

export default PatientProfile;