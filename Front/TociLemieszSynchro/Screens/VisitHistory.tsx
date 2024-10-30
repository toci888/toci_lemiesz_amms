import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, StyleSheet, Button } from 'react-native';

const VisitHistory = () => {
    const [language, setLanguage] = useState('English');
    const [visits, setVisits] = useState([]);

    useEffect(() => {
        fetchVisits();
    }, []);

    const fetchVisits = async () => {
        try {
            const response = await fetch('https://yourapi.com/api/visits');
            const data = await response.json();
            setVisits(data);
        } catch (error) {
            console.error('Error fetching visits:', error);
        }
    };

    const handleLanguageChange = (lang) => {
        setLanguage(lang);
    };

    return (
        <View style={styles.container}>
            <Text style={styles.header}>
                {language === 'English' ? 'Visit History' : 'Historia wizyt'}
            </Text>
            <FlatList
                data={visits}
                renderItem={({ item }) => (
                    <View style={styles.visitItem}>
                        <Text style={styles.visitDate}>{item.date}</Text>
                        <Text style={styles.visitNotes}>{item.notes}</Text>
                    </View>
                )}
                keyExtractor={(item) => item.id.toString()}
                style={styles.visitsList}
            />
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
    visitsList: {
        flex: 1,
        marginBottom: 10,
    },
    visitItem: {
        padding: 10,
        borderRadius: 5,
        backgroundColor: '#e1f5fe',
        marginVertical: 5,
    },
    visitDate: {
        fontWeight: 'bold',
    },
    visitNotes: {
        marginTop: 5,
    },
    languageContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
});

export default VisitHistory;