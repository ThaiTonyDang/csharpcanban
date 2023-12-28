using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace TestAlgoth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] numbers = { 1, 2, 3, 4, 5 };
            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    numbers[i] = numbers[numbers.Length - 1 - i];
            //}
            //Console.WriteLine(string.Join(", ", numbers));
            //string[] words = { "sun", "moon", "star" };
            //string result = "";
            //for (int i = 0; i < words.Length; i++)
            //{
            //    result += words[i].Substring(0, 2);
            //}
            //Console.WriteLine(result);

            var person = new Person();
            var man = new Man("Noah", 32, "VietNam", "Black");
            man.Introduce();
            man.DescribeHabit();
            var man_1 = new Man()
            {
                Name = "Vu",
                Age = 23,
                Height = 1.6f,
                Weight = 50f,
                Nationality = "VN",
            };
            man_1.Introduce();
            person.Name = "Noah";

            man.DescribeHabit();

            //try
            //{
            //    person.Age = -10;
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            try
            {
                person.Age = 30; // Gán một giá trị hợp lệ cho Age
                Console.WriteLine($"{person.Name} is {person.Age} years old.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }

            var list = new List<Person>() { person, man };
            Child parent = new Child();
            parent.Greet();
        }
    }

    public class Person
    {
        // Các trường dữ liệu không thể truy cập trực tiếp  private 
        private int _age;
        private string _password;

        // Các thuộc tính của lớp có thể truy cập
        public string Name { get; set; }
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Age must between 0 and 150");
                }
                _age = value;
            }
        }

        public string Nationality { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public Gender Gender { get; protected set; }

        // Hàm tạo hàm tạo (Contructor) không tham số và có tham số . tùy thuộc cách dùng
        public Person() { }
        public Person(string name, int age, string nationality)
        {
            this.Name = name;
            this.Age = age;
            this.Nationality = nationality;
        }

        // Các phương thức hay hành vi

        public virtual void Introduce()
        {
            Console.WriteLine($"Hello, my name is {Name}. I am {Age} years old. I am a {Nationality}");
        }

        public virtual void DescribeHabit()
        {
            Console.WriteLine("Humans have various habits.");
        }
    }

    public class Man : Person, IWorker, IACtion
    {
        public string BeardColor { get; set; }
        public string JobTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Man()
        {
            this.Gender = Gender.Male;
        }
        public Man(string name, int age, string nationality, string beardColor) : base(name, age, nationality)
        {
            this.BeardColor = beardColor;
            this.Gender = Gender.Male;
        }

        public override void Introduce()
        {
            base.Introduce();
            Console.WriteLine($"I am a man with a {BeardColor} beard and weight {Weight}.");
        }
        public new void DescribeHabit()
        {
            base.DescribeHabit();
        }


        public void Work()
        {
            throw new NotImplementedException();
        }

        public void ACtion()
        {
            throw new NotImplementedException();
        }
    }

    public class Women : Person
    {
        public string HairClolor { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Bede = 2
    }
    public interface IWorker
    {
        string JobTitle { get; set; }
        void Work();
    }
    public interface IACtion
    {
        void ACtion();
    }

    public class Parent
    {
        public Parent()
        {
            Console.WriteLine("Parent constructor.");
        }

        public virtual void Greet()
        {
            Console.WriteLine("Hello from Parent.");
        }
    }

    public class Child : Parent
    {
        public Child()
        {
            Console.WriteLine("Child constructor.");
        }

        public override void Greet()
        {
            Console.WriteLine("Hello from Child.");
        }
    }

}
