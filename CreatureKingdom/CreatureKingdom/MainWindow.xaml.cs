using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreatureKingdom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Creature> creatures;
        public MainWindow()
        {
            InitializeComponent();

            Creature creature;

            creatures = new List<Creature>();


            // Test your creature - create each creature below

            // Example:
            creature = new ChristopherChandlerCreature(kingdom, Dispatcher, 100);
            creature.Paused = true;
            creatures.Add(creature);
            creature.Place(200, 200);

            // creature = new ChristopherChandlerCreature(kingdom, Dispatcher, 100);
            // creature.Paused = true;
            // creatures.Add(creature);
            // creature.Place(200, 300);


            //OR: test all students' creatures at once

            //var kingdomAssembly = System.Reflection.Assembly.GetAssembly(typeof(Creature));
            //var creatureTypes = kingdomAssembly.DefinedTypes.Where(x => x.IsSubclassOf(typeof(Creature)));
            //foreach (var creatureType in creatureTypes)
            //{
            //    creatures.Add(Activator.CreateInstance(creatureType, kingdom, Dispatcher, 100) as Creature);
            //}

            //foreach (var myCreature in creatures)
            //{
            //    myCreature.Paused = true;
            //    myCreature.Place(200, 300);
            //}
        }

        private void PauseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            status_label.Content = "Paused";

            foreach (Creature creature in creatures)
            {
                creature.Paused = true;
            }
        }

        private void PlayMenuItem_Click(object sender, RoutedEventArgs e)
        {
            status_label.Content = "Playing";

            foreach (Creature creature in creatures)
            {
                creature.Paused = false;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Creature creature in creatures)
            {
                creature.Shutdown();
            }
        }
    }
}

