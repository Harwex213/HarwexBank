namespace HarwexBank.Languages
{
    public class ChangeLanguage
    {
        public ChangeLanguage()
        {
            App.Language = App.Language.Name == App.Languages[1].Name ? App.Languages[0] : App.Languages[1];
        }
    }
}