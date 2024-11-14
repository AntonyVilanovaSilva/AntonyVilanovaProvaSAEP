use master
go
Create database GerenciamentoTarefaDB
use GerenciamentoTarefaDB
go

Create table Usuario (
	IDUsuario int identity(1,1) primary key not null,
	Nome varchar(200) not null,
	Email varchar(200) not null
)
go

Create table StatusTarefa(
   IdStatus int identity(1,1) primary key not null,
   Descricao varchar(250) not null,
)
go

Create table Prioridade(
   IdPrioridade int identity(1,1) primary key not null,
   Descricao varchar(250) not null,
)

Create table Tarefa(
	IDTarefa int identity(1,1) primary key not null,
	Descricao varchar(250) not null,
	NomeSetor varchar(250) not null,
	DataCadastro datetime not null,
    IdStatus int,
    IdUsuario int,
	IdPrioridade int

	Constraint FK_StatusTarefa foreign key (IdStatus) references StatusTarefa(IdStatus),
	Constraint FK_UsuarioTarefa foreign key (IdUsuario) references Usuario(IdUsuario),
	Constraint FK_PrioridadeTarefa foreign key (IdPrioridade) references Prioridade(IdPrioridade)
)
go

Insert StatusTarefa(Descricao) values ('Fazer')
Insert StatusTarefa(Descricao) values ('Fazendo')
Insert StatusTarefa(Descricao) values ('Pronto')
---------------------//----------------------------
Insert Prioridade(Descricao) values ('Alta')
Insert Prioridade(Descricao) values ('Baixa')
Insert Prioridade(Descricao) values ('Media')

