namespace Data
{
    public class Repo
    {
        public string GetNameFromDb(){
            Console.WriteLine("podaj swoją nazwę: ");
            var name = Console.ReadLine();
            return name;
        }
    }
}
