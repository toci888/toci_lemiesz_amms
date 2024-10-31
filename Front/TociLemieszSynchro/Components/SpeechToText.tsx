import React, { useEffect, useState } from 'react';
import { View, Text, Button, PermissionsAndroid, Platform } from 'react-native';
import Voice from 'react-native-voice';
import axios from 'axios';

const SpeechToText = () => {
    const [recognizedText, setRecognizedText] = useState('');
    const [isListening, setIsListening] = useState(false);

    useEffect(() => {
        Voice.onSpeechResults = onSpeechResults;
        Voice.onSpeechError = onSpeechError;

        return () => {
            Voice.destroy().then(Voice.removeAllListeners);
        };
    }, []);

    const onSpeechResults = (event) => {
        const text = event.value[0];
        setRecognizedText(text);
    };

    const onSpeechError = (event) => {
        console.error(event.error);
    };

    const startListening = async () => {
        if (Platform.OS === 'android') {
            const granted = await PermissionsAndroid.request(PermissionsAndroid.PERMISSIONS.RECORD_AUDIO);
            if (granted !== PermissionsAndroid.RESULTS.GRANTED) {
                console.error('Audio permission denied');
                return;
            }
        }

        setIsListening(true);
        try {
            await Voice.start('pl-PL');
        } catch (error) {
            console.error('Error starting Voice:', error);
        }
    };

    const stopListening = async () => {
        setIsListening(false);
        try {
            await Voice.stop();
        } catch (error) {
            console.error('Error stopping Voice:', error);
        }
    };

    const sendToGCP = async (audioData) => {
        const apiKey = 'AIzaSyCumg7llWz5y90fsJYlcxytqSS8IxMbHmI'; // Replace with your actual API key
        const url = `https://speech.googleapis.com/v1/speech:recognize?key=${apiKey}`;

        const body = {
            config: {
                encoding: 'LINEAR16',
                sampleRateHertz: 16000,
                languageCode: 'pl-PL',
            },
            audio: {
                content: audioData, // Base64 encoded audio data
            },
        };

        try {
            const response = await axios.post(url, body);
            console.log('GCP Response:', response.data);
            // Process the response as needed
        } catch (error) {
            console.error('Error sending audio to GCP:', error);
        }
    };

    return (
        <View>
            <Text>Recognized Text: {recognizedText}</Text>
            <Button title={isListening ? "Stop Listening" : "Start Listening"} onPress={isListening ? stopListening : startListening} />
        </View>
    );
};

export default SpeechToText;