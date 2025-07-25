using CrudSqlServerDapper.Entities;
using CrudSqlServerDapper.Settings;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSqlServerDapper.Repositories
{
    /// <summary>
    /// Repositório de dados para CRUD de clientes
    /// em um banco de dados do SqlServer
    /// </summary>
    public class ClientRepository
    {
        /// <summary>
        /// Atributo privado que contem uma referencia da classe AppSettings
        /// </summary>
        private AppSettings _appSettings = new AppSettings();

        /// <summary>
        /// Método para receber um registro de cliente
        /// e inserir os dados na tabela do banco sqlserver
        /// </summary>
        public void Insert(Client client)
        {
            //Escrevendo o comando SQL
            var query = @"
                    INSERT INTO CLIENT(ID, NAME, EMAIL, BIRTHDATE)
                    VALUES(@Id, @Name, @Email, @Birthdate)
                ";

            //Abrindo conexão com o banco de dados
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                //Executando o comando SQL no banco de dados
                connection.Execute(query, client);
            }
        }

        /// <summary>
        /// Método para atualizar um cliente no banco de dados
        /// </summary>
        public void Update(Client client)
        {
            //Escrevendo o comando SQL
            var query = @"
                UPDATE CLIENT
                SET
                    NAME = @Name,
                    EMAIL = @Email,
                    BIRTHDATE = @Birthdate
                WHERE
                    ID = @Id
            ";

            //Conectando no banco de dados
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, client);
            }
        }

        /// <summary>
        /// Método para excluir um cliente no banco de dados
        /// </summary>
        public void Delete(Guid id)
        {
            //Escrevendo o comando SQL
            var query = @"
                DELETE FROM CLIENT
                WHERE ID = @Id
            ";

            //Conectando no banco de dados
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                connection.Execute(query, new { id });
            }
        }

        /// <summary>
        /// Método para retonar uma lista com todos os clientes
        /// que estão cadastrados na tabela do banco de dados
        /// </summary>
        public List<Client> GetAll()
        {
            //Escrevendo o comando SQL
            var query = @"
                SELECT ID, NAME, EMAIL, BIRTHDATE
                FROM CLIENT
                ORDER BY NAME
            ";

            //Conectando no banco de dados
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                return connection.Query<Client>(query).ToList();
            }
        }

        /// <summary>
        /// Método para consultar e retornar 1 cliente do banco de dados
        /// baseado no ID informado ou retornar null se nenhum cliente
        /// for encontrado.
        /// </summary>
        public Client? GetById(Guid id)
        {
            //Escrevendo o comando SQL
            var query = @"
                SELECT ID, NAME, EMAIL, BIRTHDATE
                FROM CLIENT
                WHERE ID = @id
            ";

            //Conectando no banco de dados
            using (var connection = new SqlConnection(_appSettings.ConnectionString))
            {
                return connection.QueryFirstOrDefault<Client>(query, new { id });
            }
        }
    }
}
