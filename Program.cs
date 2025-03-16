using Animal.infrastructure;
using Animal.infrastructure.Servises;
using Animal.Infrastructure.Entities;
using Animal.Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Animal.Infrastructure.Interfaces;
using Animal.infrastructure.Interface;
using System.Net.WebSockets;
namespace ConsoleApp36
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var sp = DIConfiguration.GetServiceProvider();
            Console.WriteLine("Передаю вітання нашим друзям Броненосцям. :)!");
            var animalService = sp.GetService<IAnimalService>();





            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Додати новий вид");
                Console.WriteLine("2. Оновити вид");
                Console.WriteLine("3. Видалити вид");
                Console.WriteLine("4. Переглянути всі види");
                Console.WriteLine("5. Вихід");
                Console.Write("Виберіть опцію: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Невірний ввід!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        Console.Write("Введіть назву нового виду: ");
                        string? name = Console.ReadLine();
                        Console.Write("Введіть вагу: ");
                        if (double.TryParse(Console.ReadLine(), out double weight))
                        {
                            Console.Write("Введіть ID категорії виду: ");
                            if (int.TryParse(Console.ReadLine(), out int specieId))
                            {
                                var newSpecie = animalService.CreateSpecie(name!, "", (int)weight, specieId);
                                Console.WriteLine(newSpecie != null ? "Вид успішно додано." : "Помилка при додаванні.");
                            }
                        }
                        break;

                    case 2:
                        Console.Write("Введіть ID виду для оновлення: ");
                        if (int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            var specieToUpdate = animalService.GetSpecieById(updateId);
                            if (specieToUpdate != null)
                            {
                                Console.Write("Введіть нову назву: ");
                                string? newName = Console.ReadLine();
                                Console.Write("Введіть нову вагу: ");
                                if (double.TryParse(Console.ReadLine(), out double newWeight))
                                {
                                    bool updated = animalService.UpdateSpecie(updateId, newName!, "", (int)newWeight, specieToUpdate.SpecieId);
                                    Console.WriteLine(updated ? "Вид оновлено." : "Помилка при оновленні.");
                                }
                            }
                        }
                        break;

                    case 3:
                        Console.Write("Введіть ID виду для видалення: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            Console.WriteLine(animalService.DeleteSpecie(deleteId) ? "Вид успішно видалено." : "Помилка при видаленні.");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Список всіх видів:");
                        foreach (var speci in animalService.GetAllSpecies())
                        {
                            Console.WriteLine($"ID: {speci.Id}, Назва: {speci.Name}, Вага: {speci.Weight} кг");
                        }
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Невідома команда!");
                        break;
                }
            }


            //int choice = 0;
            //while (true)
            //{
            //    Console.WriteLine("\nВиберіть дію:");
            //    Console.WriteLine("1 - Додати вид");
            //    Console.WriteLine("2 - Оновити вид");
            //    Console.WriteLine("3 - Видалити вид");
            //    Console.WriteLine("4 - Показати всі види");
            //    Console.WriteLine("5 - Вийти");
            //    Console.Write("Ваш вибір: ");

            //    if (!int.TryParse(Console.ReadLine(), out choice))
            //    {
            //        Console.WriteLine("Неправильний ввід! Введіть число.");
            //        continue;
            //    }

            //switch (choice)
            //{
            //    case 1:
            //        Console.Write("Введіть назву нового виду: ");
            //        string? name = Console.ReadLine();
            //        if (!string.IsNullOrWhiteSpace(name))
            //        {
            //            var newSpecie = new Specie { Name = name };
            //            specieService.Add(newSpecie);
            //            Console.WriteLine($"Вид '{name}' успішно додано.");
            //        }
            //        else
            //        {
            //            Console.WriteLine("Назва не може бути порожньою!");
            //        }
            //        break;

            //    case 2:
            //        Console.Write("Введіть ID виду для оновлення: ");
            //        if (int.TryParse(Console.ReadLine(), out int updateId))
            //        {
            //            var specieToUpdate = specieService.GetById(updateId);
            //            if (specieToUpdate != null)
            //            {
            //                Console.Write("Введіть нову назву: ");
            //                string? newName = Console.ReadLine();
            //                if (!string.IsNullOrWhiteSpace(newName))
            //                {
            //                    specieToUpdate.Name = newName;
            //                    specieService.Update(specieToUpdate);
            //                    Console.WriteLine("Вид оновлено.");
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Назва не може бути порожньою!");
            //                }
            //            }
            //            else
            //            {
            //                Console.WriteLine("Вид з таким ID не знайдено!");
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("Неправильний ID!");
            //        }
            //        break;

            //    case 3:
            //        Console.Write("Введіть ID виду для видалення: ");
            //        if (int.TryParse(Console.ReadLine(), out int deleteId))
            //        {
            //            try
            //            {
            //                specieService.Delete(deleteId);
            //                Console.WriteLine($"Вид з ID {deleteId} успішно видалено.");
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine($"Помилка: {ex.Message}");
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine("Неправильний ID!");
            //        }
            //        break;

            //    case 4:
            //        Console.WriteLine("Список всіх видів:");
            //        var species = specieService.GetAll();
            //        foreach (var speci in species)
            //        {
            //            Console.WriteLine($"ID: {speci.Id}, Назва: {speci.Name}");
            //        }
            //        break;

            //    case 5:
            //        Console.WriteLine("Вихід...");
            //        return;

            //    default:
            //        Console.WriteLine("Невідома команда! Спробуйте ще раз.");
            //        break;
            //}
            //}

        }
    }
}
