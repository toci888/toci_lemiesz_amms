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
    s."SessionId",
    u1."Name" AS patient_name,
    u2."Name" AS psychiatrist_name,
    s."SessionStart",
    s."SessionEnd",
    EXTRACT(EPOCH FROM (s."SessionEnd" - s."SessionStart")) / 60 AS session_duration_minutes,
    LENGTH(s."ChatGPTSummary") AS summary_length,
    COUNT(c."EntryId") AS total_messages
FROM
    "Sessions" s
JOIN
    "Users" u1 ON s."PatientId" = u1."UserId"
JOIN
    "Users" u2 ON s."PsychiatristId" = u2."UserId"
LEFT JOIN
    "ConversationEntries" c ON s."SessionId" = c."SessionId"
GROUP BY
    s."SessionId", u1."Name", u2."Name";

