// See https://aka.ms/new-console-template for more information

using System.Collections;
using System.Net.WebSockets;

ICollection values = new List<Person>();

IEnumerable person = new List<Person>()
{
    new Person
    {
        Id = 1,
        Name = "Thai",
        Age = 30
    },
    new Person
    {
        Id = 2,
        Name = "Thuy",
        Age = 24
    },
     new Person
    {
        Id = 3,
        Name = "Thanh",
        Age = 60
    },
       new Person
    {
        Id = 4,
        Name = "Thành",
        Age = 67
    }
};

var s = person.Cast<Person>().ToList();


IEnumerable<Person> person_1 = new List<Person>()
{
    new Person
    {
        Id = 1,
        Name = "Thai",
        Age = 30
    },
    new Person
    {
        Id = 2,
        Name = "Thuy",
        Age = 30
    }
};

// IEnumrable chỉ muốn duyệt mà không muốn làm gì cả

var listPerson = new List<Person>()
{
    new Person
    {
        Id = 1,
        Name = "Thai",
        Age = 30
    },
    new Person
    {
        Id = 2,
        Name = "Thuy",
        Age = 28
    },
     new Person
    {
        Id = 3,
        Name = "Thanh",
        Age = 60
    },
       new Person
    {
        Id = 4,
        Name = "Thành",
        Age = 67
    },
       new Person
    {
        Id = 5,
        Name = "Nhiên",
        Age = 29
    },
        new Person
    {
        Id = 5,
        Name = "Phong",
        Age = 1
    },
};

var enumrator = listPerson.GetEnumerator();


while (enumrator.MoveNext())
{
    var name = (Person)enumrator.Current;
    Console.WriteLine(name.Name + "   ");
}

IEnumerator<Person> enumeratorT = listPerson.GetEnumerator();

while (enumeratorT.MoveNext())
{
    var name = enumeratorT.Current;
    Console.WriteLine(name);
}


class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

