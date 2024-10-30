import React, { useState } from 'react';
import { View, Text, TextInput, Button, FlatList, StyleSheet } from 'react-native';

const TherapeuticNotes = () => {
    const [note, setNote] = useState('');
    const [notes, setNotes] = useState([]);
    const [language, setLanguage] = useState('English'); // Default language

    const addNote = () => {
        if (note.trim()) {
            setNotes(prevNotes => [...prevNotes, { id: Date.now().toString(), text: note }]);
            setNote('');
        }
    };

    const handleLanguageChange = (lang) => {
        setLanguage(lang);
    };

    return (
        <View style={styles.container}>
            <Text style={styles.header}>
                {language === 'English' ? 'Therapeutic Notes' : 'Notatki terapeutyczne'}
            </Text>
            <FlatList
                data={notes}
                renderItem={({ item }) => <Text style={styles.note}>{item.text}</Text>}
                keyExtractor={item => item.id}
                style={styles.notesList}
            />
            <TextInput
                style={styles.input}
                value={note}
                onChangeText={setNote}
                placeholder={language === 'English' ? 'Enter your note...' : 'Wprowadź notatkę...'}
            />
            <Button title={language === 'English' ? 'Add Note' : 'Dodaj notatkę'} onPress={addNote} />
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
    notesList: {
        flex: 1,
        marginBottom: 10,
    },
    note: {
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

export default TherapeuticNotes;