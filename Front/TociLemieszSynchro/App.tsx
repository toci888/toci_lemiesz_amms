import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import PatientProfile from './Screens/PatientProfile';
import VisitHistory from './Screens/VisitHistory';
import TherapeuticNotes from './Screens/TherapeuticNotes';
import MedicationManagement from './Screens/MedicationManagement';
import Chat from './Screens/Chat';
import Home from './Screens/Home';

const Stack = createNativeStackNavigator();

const App = () => {
    return (
        <NavigationContainer>
            <Stack.Navigator initialRouteName="Home">
                <Stack.Screen name="Home" component={Home} />
                <Stack.Screen name="PatientProfile" component={PatientProfile} />
                <Stack.Screen name="VisitHistory" component={VisitHistory} />
                <Stack.Screen name="TherapeuticNotes" component={TherapeuticNotes} />
                <Stack.Screen name="MedicationManagement" component={MedicationManagement} />
                <Stack.Screen name="Chat" component={Chat} />
            </Stack.Navigator>
        </NavigationContainer>
    );
};

export default App;
