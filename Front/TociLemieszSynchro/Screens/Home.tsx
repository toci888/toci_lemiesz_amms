import React from 'react';
import { View, Text, Button } from 'react-native';

const Home = ({ navigation }: any) => {
    return (
        <View>
            <Text>Witaj w aplikacji psychiatrycznej!</Text>
            <Button title="Profil pacjenta" onPress={() => navigation.navigate('PatientProfile')} />
            <Button title="Historia wizyt" onPress={() => navigation.navigate('VisitHistory')} />
            <Button title="Notatki terapeutyczne" onPress={() => navigation.navigate('TherapeuticNotes')} />
            <Button title="Zarządzanie lekami" onPress={() => navigation.navigate('MedicationManagement')} />
            <Button title="Czat terapeutyczny" onPress={() => navigation.navigate('Chat')} />
        </View>
    );
};

export default Home;
