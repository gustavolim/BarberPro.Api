CREATE TABLE IF NOT EXISTS Usuario (
    Id TEXT PRIMARY KEY,
    Nome TEXT NOT NULL,
    Email TEXT NOT NULL,
    EhBarbeiro INTEGER NOT NULL,
    SenhaHash TEXT
);

CREATE TABLE IF NOT EXISTS Agendamento (
    Id TEXT PRIMARY KEY,
    ClienteId TEXT NOT NULL,
    BarbeiroId TEXT NOT NULL,
    DataHora TEXT NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Usuario(Id),
    FOREIGN KEY (BarbeiroId) REFERENCES Usuario(Id)
);
