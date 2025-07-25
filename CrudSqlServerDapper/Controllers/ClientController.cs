using CrudSqlServerDapper.Entities;
using CrudSqlServerDapper.Repositories;
using CrudSqlServerDapper.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSqlServerDapper.Controllers
{
    /// <summary>
    /// Classe de controle para realizar operações de gravação, edição,
    /// exclusão e consulta de clientes para o usuário do sistema.
    /// </summary>
    public class ClientController
    {
        /// <summary>
        /// Método para executar as operações do controlador
        /// </summary>
        public void Execute()
        {
            Console.WriteLine("\nSISTEMA DE CONTROLE DE CLIENTES:\n");

            Console.WriteLine("(1) CADASTRAR CLIENTE");
            Console.WriteLine("(2) ATUALIZAR CLIENTE");
            Console.WriteLine("(3) EXCLUIR CLIENTE");
            Console.WriteLine("(4) CONSULTAR CLIENTES");

            Console.Write("\nINFORME A OPÇÃO DESEJADA...: ");
            var opcao = Console.ReadLine();

            switch(opcao)
            {
                case "1": //caso a opção seja "1"
                    CreateClient(); //executando o método para cadastrar cliente
                    break;

                case "2": //caso a opção seja "2"
                    UpdateClient(); //executando o método para atualizar cliente
                    break;

                case "3": //caso a opção seja "3"
                    DeleteClient(); //executando o método para excluir cliente
                    break;

                case "4": //caso a opção seja "4"
                    ReadClients(); //executando o método para consultar os clientes
                    break;

                default: //caso não seja nenhum dos anteriores
                    Console.WriteLine("\nOPÇÃO INVÁLIDA!");
                    break;
            }

            Console.Write("\nDESEJA CONTINUAR? (S,N): ");
            var continuar = Console.ReadLine() ?? string.Empty;

            if(continuar.Equals("S", StringComparison.OrdinalIgnoreCase))
            {                
                Console.Clear(); //Limpar a tela do console do DOS (Prompt)
                Execute(); //Voltar para o início do método
            }
            else
            {
                Console.WriteLine("\nFIM DO PROGRAMA!");
            }
        }

        /// <summary>
        /// Método para realizar o cadastro de um cliente
        /// </summary>
        private void CreateClient()
        {
            Console.WriteLine("\nCADASTRO DE CLIENTE:\n");

            //Criando um objeto da classe de entidade
            var client = new Client();

            Console.Write("INFORME O NOME...............: ");
            client.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("INFORME O EMAIL..............: ");
            client.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("INFORME A DATA DE NASCIMENTO.: ");
            client.BirthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

            //Instanciando a classe de validação do cliente
            var validator = new ClientValidator();
            //Executar as validações no objeto e capturar o resultado
            var result = validator.Validate(client);

            //Verificar os dados do cliente passaram nas regras de validação
            if(result.IsValid)
            {
                //Criando um objeto da classe de repositório
                var clientRepository = new ClientRepository();
                clientRepository.Insert(client); //gravando o cliente

                Console.WriteLine("\nCLIENTE CADASTRADO COM SUCESSO!");
            }
            else
            {
                Console.WriteLine("\nOCORRERAM ERROS DE VALIDAÇÃO!");
                foreach (var item in result.Errors)
                {
                    Console.WriteLine($"\tErro: {item.ErrorMessage}");
                }
            }            
        }

        /// <summary>
        /// Método para realizar a atualização do cliente
        /// </summary>
        private void UpdateClient()
        {
            Console.WriteLine("\nEDIÇÃO DE CLIENTES:\n");

            Console.Write("INFORME O ID DO CLIENTE......: ");
            var id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            //consultando o cliente no banco de dados através do ID
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(id);

            //verificar se o cliente foi encontrado
            if(client != null)
            {
                Console.WriteLine("\nDADOS DO CLIENTE:");

                Console.WriteLine($"\tID...........: {client.Id}");
                Console.WriteLine($"\tNAME.........: {client.Name}");
                Console.WriteLine($"\tEMAIL........: {client.Email}");
                Console.WriteLine($"\tBIRTHDATE....: {client.BirthDate}");

                Console.WriteLine("\nINFORME OS DADOS PARA EDIÇÃO:\n");

                Console.Write("INFORME O NOME...............: ");
                client.Name = Console.ReadLine() ?? string.Empty;

                Console.Write("INFORME O EMAIL..............: ");
                client.Email = Console.ReadLine() ?? string.Empty;

                Console.Write("INFORME A DATA DE NASCIMENTO.: ");
                client.BirthDate = DateTime.Parse(Console.ReadLine() ?? string.Empty);

                //Instanciando a classe de validação do cliente
                var validator = new ClientValidator();
                //Executar as validações no objeto e capturar o resultado
                var result = validator.Validate(client);

                //Verificar os dados do cliente passaram nas regras de validação
                if (result.IsValid)
                {                    
                    clientRepository.Update(client); //atualizando o cliente

                    Console.WriteLine("\nCLIENTE ATUALIZADO COM SUCESSO!");
                }
                else
                {
                    Console.WriteLine("\nOCORRERAM ERROS DE VALIDAÇÃO!");
                    foreach (var item in result.Errors)
                    {
                        Console.WriteLine($"\tErro: {item.ErrorMessage}");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nCLIENTE NÃO FOI ENCONTRADO.");
            }
        }

        /// <summary>
        /// Método para realizar a exclusão do cliente
        /// </summary>
        private void DeleteClient()
        {
            Console.WriteLine("\nEXCLUSÃO DE CLIENTES:\n");

            Console.Write("INFORME O ID DO CLIENTE......: ");
            var id = Guid.Parse(Console.ReadLine() ?? string.Empty);

            //consultando o cliente no banco de dados através do ID
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(id);

            //verificar se o cliente foi encontrado
            if (client != null)
            {
                Console.WriteLine("\nDADOS DO CLIENTE:");

                Console.WriteLine($"\tID...........: {client.Id}");
                Console.WriteLine($"\tNAME.........: {client.Name}");
                Console.WriteLine($"\tEMAIL........: {client.Email}");
                Console.WriteLine($"\tBIRTHDATE....: {client.BirthDate}");

                Console.WriteLine("\nDESEJA EXCLUIR O CLIENTE? (S,N): ");
                var opcao = Console.ReadLine() ?? string.Empty;

                if(opcao.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    clientRepository.Delete(id);

                    Console.WriteLine("\nCLIENTE EXCLUÍDO COM SUCESSO!");
                }
            }
            else
            {
                Console.WriteLine("\nCLIENTE NÃO FOI ENCONTRADO.");
            }
        }

        /// <summary>
        /// Método para realizar a consulta dos clientes
        /// </summary>
        private void ReadClients()
        {
            Console.WriteLine("\nCONSULTA DE CLIENTES:\n");

            //Criando um objeto da classe de repositório
            var clientRepository = new ClientRepository();
            
            //consultar e obter uma lista de clientes
            var clients = clientRepository.GetAll();

            //exibir cada cliente obtido do banco de dados
            foreach (var item in clients)
            {
                Console.WriteLine($"ID..........: {item.Id}");
                Console.WriteLine($"NAME........: {item.Name}");
                Console.WriteLine($"EMAIL.......: {item.Email}");
                Console.WriteLine($"BIRTHDATE...: {item.BirthDate.ToString("dd/MM/yyyy")}");
                Console.WriteLine("...");
            }
        }
    }
}
