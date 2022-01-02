using System;
using System.Collections.Generic;

namespace ChainResponsability
{

    public class Mobile
    {
        public Mobile(string name, Type type, double price)
        {
            Name = name;
            this.type = type;
            Price = price;
        }

        public string Name { get; private set; }
        public Type type { get; private set; }
        public double Price { get; private set; }

        public override string ToString()
        {
            return $"{Name}, Categoria: {type}, Precio: {Price}";
        }
    }

    abstract class Handler {
        protected Handler _succesor;
        protected ISpecification<Mobile> _specification;

        protected Handler(ISpecification<Mobile> specification)
        {
            _specification = specification;
        }

        public void SetSuccesor(Handler succesor)
        {
            this._succesor = succesor;
        }

        public abstract void HandleRequest(Mobile mobile);
    }

    class Employee : Handler
    {
        public Employee(ISpecification<Mobile> specification) : base(specification)
        {
        }

        public override void HandleRequest(Mobile mobile)
        {
            if (CandHandle(mobile))
            {
                Console.WriteLine($"La orden de {mobile.Name} realizada por {this.GetType().Name}");
            }
            else
            {
                _succesor.HandleRequest(mobile);
            }
        }

        public bool CandHandle(Mobile mobile)
        {
            return _specification.IsSatisfied(mobile);
        }
    }

    class Supervisor : Handler
    {
        public Supervisor(ISpecification<Mobile> specification) : base(specification)
        {
        }

        public override void HandleRequest(Mobile mobile)
        {
            if (CandHandle(mobile))
            {
                Console.WriteLine($"La orden de {mobile.Name} realizada por {this.GetType().Name}");
            }
            else
            {
                _succesor.HandleRequest(mobile);
            }
        }

        public bool CandHandle(Mobile mobile)
        {
            return _specification.IsSatisfied(mobile);
        }
    }

    class CEO : Handler
    {
        public CEO(ISpecification<Mobile> specification) : base(specification)
        {
        }

        public override void HandleRequest(Mobile mobile)
        {
            if (CandHandle(mobile))
            {
                Console.WriteLine($"La orden de {mobile.Name} realizada por {this.GetType().Name}");
            }
            else
            {
                _succesor.HandleRequest(mobile);
            }
        }

        public bool CandHandle(Mobile mobile)
        {
            return _specification.IsSatisfied(mobile);
        }
    }

    public enum Type
    {
        Basic,
        Medium,
        Premium
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CHAIN RESPONSABILITY" + "\n");
            Console.WriteLine("Al recibir una solicitud, cada manejador decide si la procesa o si la pasa al siguiente manejador de la cadena" + "\n");

            var s8 = new Mobile("Galaxy S8", Type.Medium, 12000);
            var motorola = new Mobile("Moto G", Type.Basic, 500);
            var iphone = new Mobile("Iphone", Type.Premium, 30000);

            var phones = new List<Mobile>();
            phones.Add(s8); phones.Add(motorola); phones.Add(iphone);

            var employee = new Employee(new MobileBasic());
            var supervisor = new Supervisor(new MobileMedium());
            var ceo = new CEO(new MobilePremium());

            employee.SetSuccesor(supervisor);
            supervisor.SetSuccesor(ceo);

            phones.ForEach(t => employee.HandleRequest(t));

            Console.ReadLine();
        }
    }
}
