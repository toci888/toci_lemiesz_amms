-- Tabela u¿ytkowników (psychiatrzy i pacjenci)
CREATE TABLE users (
    user_id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    role VARCHAR(50) CHECK (role IN ('patient', 'psychiatrist')) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabela sesji (ka¿da sesja to oddzielne spotkanie)
CREATE TABLE sessions (
    session_id SERIAL PRIMARY KEY,
    patient_id INT REFERENCES users(user_id),
    psychiatrist_id INT REFERENCES users(user_id),
    session_start TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    session_end TIMESTAMP,
    chatgpt_summary TEXT, -- Przechowuje automatyczne podsumowanie od ChatGPT
    audio_transcription TEXT, -- Transkrypcja rozmowy z Google Voice Recognition
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Tabela zapisuj¹ca poszczególne wiadomoœci z konwersacji
CREATE TABLE conversation_entries (
    entry_id SERIAL PRIMARY KEY,
    session_id INT REFERENCES sessions(session_id),
    sender_id INT REFERENCES users(user_id),
    message_text TEXT,
    message_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_voice BOOLEAN DEFAULT FALSE, -- Wskazuje, czy wiadomoœæ pochodzi³a z transkrypcji g³osowej
    chatgpt_response TEXT -- Zawiera odpowiedŸ generowan¹ przez ChatGPT, jeœli dotyczy
);

-- Indeksy dla wydajnoœci
CREATE INDEX idx_session_id ON conversation_entries(session_id);
CREATE INDEX idx_sender_id ON conversation_entries(sender_id);
CREATE INDEX idx_role ON users(role);

-- Widok do raportowania, np. dla analiz czasu trwania sesji
CREATE VIEW session_report AS
SELECT
    s.session_id,
    u1.name AS patient_name,
    u2.name AS psychiatrist_name,
    s.session_start,
    s.session_end,
    EXTRACT(EPOCH FROM (s.session_end - s.session_start)) / 60 AS session_duration_minutes,
    LENGTH(s.chatgpt_summary) AS summary_length,
    COUNT(c.entry_id) AS total_messages
FROM
    sessions s
JOIN
    users u1 ON s.patient_id = u1.user_id
JOIN
    users u2 ON s.psychiatrist_id = u2.user_id
LEFT JOIN
    conversation_entries c ON s.session_id = c.session_id
GROUP BY
    s.session_id, u1.name, u2.name;

