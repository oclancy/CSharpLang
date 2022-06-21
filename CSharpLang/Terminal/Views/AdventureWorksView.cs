using AdventureWorks.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;

namespace Terminal.Views
{
    internal class AdventureWorksView : IView
    {
        internal const string NAME = "AdventureWorks";
        public string Id => NAME;

        internal Window View { get; private set; }

        View IView.View => View;

        private ListView Customers = new ListView();

        public AdventureWorksView()
        {
            View = new Window(Id);

            var apply = new Button(1, 0, "_Get");
            apply.Clicked += Apply_Clicked;

            Customers = new ListView()
            {
                Height = Dim.Fill(),
                Width = Dim.Fill(),
                Y = Pos.Bottom(apply)
            };

            void Apply_Clicked()
            {
                using var context = new AdventureWorks.Db.Models.AdventureWorksLT2019Context();

                var allCustomers = context.Customers.ToList();


                Customers.SetSource(allCustomers.Select(c => c.EmailAddress).ToList());
                
            }

            View.Add(apply,Customers);
        }
    }
}
