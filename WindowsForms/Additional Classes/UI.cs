
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsForms
{
    class UI
    {
            public static void Hide(params Control[] controlElements)
            {
                foreach (var item in controlElements)
                {
                    item.Visible = false;
                }
            }

            public static void Show(params Control[] controlElements)
            {
                foreach (var item in controlElements)
                {
                    item.Visible = true;
                }
            }

            public static OpenFileDialog CreateOpenFileDialog()
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "(*.xml)|*.xml",
                    RestoreDirectory = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Title = "Choose file"
                };

                return openFileDialog;
            }

            public static SaveFileDialog CreateSaveFileDialog()
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    RestoreDirectory = true,
                    DefaultExt = "xml",
                    CheckPathExists = true,
                    Title = "Save your work",
                    ValidateNames = true
                };

                return saveFileDialog;
            }

        static public string StartHints =
       "Draw a circle:\n\t -You should click two times at the panel to choose circle\n\t center.\n\t -Then click two times to choose point which lay ona circle\n\t outline.\n";
        static public string ChooseCircle = "\n\n\nPick circle:\n\t -To choose circle click mouse right button on it one time.\n";
        static public string ActionsWithCircle = "\n\n\nActions with circles:\n\t -To raplace active circle click mouse right button at new center\n\t  of this circle.\n" +
            "\t -To change color of circle press button \"Choose Color\"\n" +
            "\t -To delete active circle press button \"Delete\"\n" + "\t -If you want cancel click mouse left button.\n";


        static public string INFORMATION_MESSAGE = "General :\n" +
                StartHints + ChooseCircle + ActionsWithCircle +
                "\n\n\nFile :\n" +
                "\t -If you want to save your figures press 'Save'.\n" +
                "\t -If you want to create new picture press 'New'.\n" +
                "\t -If you want to open your existing work press 'Open'\n" +
                "\t and choose file.\n" +
                 "Shapes :\n" +
                "\t -In menu 'Shapes' you watch list of figures (color - radius)\n\t which are " +
                "on your screen at the moment'\n" +
                "\t -You could find any circle easily if you press at this in list \n";

        

    }
}
