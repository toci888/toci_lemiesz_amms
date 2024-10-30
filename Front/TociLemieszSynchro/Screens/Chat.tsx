import React, { useState } from 'react';
import { View, Text, TextInput, Button, FlatList, StyleSheet } from 'react-native';

const Chat = () => {
    const [message, setMessage] = useState('');
    const [messages, setMessages] = useState([]);

    const sendMessage = () => {
        if (message.trim()) {
            // Add the user's message to the messages array
            setMessages(prevMessages => [...prevMessages, { id: Date.now().toString(), text: message }]);
            setMessage('');

            // Here you would typically call your chat model API
            // For example:
            // fetchChatGPTResponse(message).then(response => {
            //     setMessages(prevMessages => [...prevMessages, { id: Date.now().toString(), text: response }]);
            // });
        }
    };

    return (
        <View style={styles.container}>
            <Text style={styles.header}>Czat terapeutyczny</Text>
            <FlatList
                data={messages}
                renderItem={({ item }) => <Text style={styles.message}>{item.text}</Text>}
                keyExtractor={item => item.id}
                style={styles.messagesList}
            />
            <TextInput
                style={styles.input}
                value={message}
                onChangeText={setMessage}
                placeholder="Type your message..."
            />
            <Button title="Send" onPress={sendMessage} />
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
    messagesList: {
        flex: 1,
        marginBottom: 10,
    },
    message: {
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
});

export default Chat;