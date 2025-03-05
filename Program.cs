using Animal.infrastructure;
using Animal.infrastructure.Servises;
using Animal.Infrastructure.Entities;
using Animal.Infrastructure.Services;
using System.Text;

namespace ConsoleApp36
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Передаю вітання нашим друзям Броненосцям. :)!");
            var context = new AppAnimalContext();
            var repository = new Repository<Specie>(context);
            var specieService = new SpecieService(repository, context);


            

            int choice = 0;
            while (true)
            {
                Console.WriteLine("\nВиберіть дію:");
                Console.WriteLine("1 - Додати вид");
                Console.WriteLine("2 - Оновити вид");
                Console.WriteLine("3 - Видалити вид");
                Console.WriteLine("4 - Показати всі види");
                Console.WriteLine("5 - Вийти");
                Console.Write("Ваш вибір: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Неправильний ввід! Введіть число.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Введіть назву нового виду: ");
                        string? name = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(name))
                        {
                            var newSpecie = new Specie { Name = name };
                            specieService.Add(newSpecie);
                            Console.WriteLine($"Вид '{name}' успішно додано.");
                        }
                        else
                        {
                            Console.WriteLine("Назва не може бути порожньою!");
                        }
                        break;

                    case 2:
                        Console.Write("Введіть ID виду для оновлення: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var specieToUpdate = specieService.GetById(updateId);
                            if (specieToUpdate != null)
                            {
                                Console.Write("Введіть нову назву: ");
                                string? newName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newName))
                                {
                                    specieToUpdate.Name = newName;
                                    specieService.Update(specieToUpdate);
                                    Console.WriteLine("Вид оновлено.");
                                }
                                else
                                {
                                    Console.WriteLine("Назва не може бути порожньою!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Вид з таким ID не знайдено!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неправильний ID!");
                        }
                        break;

                    case 3:
                        Console.Write("Введіть ID виду для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            try
                            {
                                specieService.Delete(deleteId);
                                Console.WriteLine($"Вид з ID {deleteId} успішно видалено.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Помилка: {ex.Message}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Неправильний ID!");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Список всіх видів:");
                        var species = specieService.GetAll();
                        foreach (var speci in species)
                        {
                            Console.WriteLine($"ID: {speci.Id}, Назва: {speci.Name}");
                        }
                        break;

                    case 5:
                        Console.WriteLine("Вихід...");
                        return;

                    default:
                        Console.WriteLine("Невідома команда! Спробуйте ще раз.");
                        break;
                }
            }
        }
    }
}
