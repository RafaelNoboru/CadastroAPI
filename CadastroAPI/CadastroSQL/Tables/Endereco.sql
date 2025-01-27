CREATE TABLE Endereco (
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    Logradouro NVARCHAR(200) NOT NULL, 
    Cep NVARCHAR(10) NOT NULL,         
    Cidade NVARCHAR(100) NOT NULL,     
    Estado NVARCHAR(2) NOT NULL,       
    PessoaId INT NOT NULL,             
    CONSTRAINT FK_Endereco_Pessoa FOREIGN KEY (PessoaId) REFERENCES Pessoa(Id) ON DELETE CASCADE
);
