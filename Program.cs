class Program
{
    static void Main(string[] args)
    {

        try
        {
            string welcome = @"
$$$$$$$\                                                                $$\               
$$  __$$\                                                               $$ |              
$$ |  $$ | $$$$$$\  $$$$$$$\ $$\    $$\  $$$$$$\  $$$$$$$\  $$\   $$\ $$$$$$\    $$$$$$\  
$$$$$$$\ |$$  __$$\ $$  __$$\\$$\  $$  |$$  __$$\ $$  __$$\ $$ |  $$ |\_$$  _|  $$  __$$\ 
$$  __$$\ $$$$$$$$ |$$ |  $$ |\$$\$$  / $$$$$$$$ |$$ |  $$ |$$ |  $$ |  $$ |    $$ /  $$ |
$$ |  $$ |$$   ____|$$ |  $$ | \$$$  /  $$   ____|$$ |  $$ |$$ |  $$ |  $$ |$$\ $$ |  $$ |
$$$$$$$  |\$$$$$$$\ $$ |  $$ |  \$  /   \$$$$$$$\ $$ |  $$ |\$$$$$$  |  \$$$$  |\$$$$$$  |
\_______/  \_______|\__|  \__|   \_/     \_______|\__|  \__| \______/    \____/  \______/ 
                                                                                          
                                                                                                                                                                                    
";
            Console.WriteLine(welcome);
            Console.WriteLine("");
            Console.Write("Inserisci il nome: ");
            string nome = Console.ReadLine();

            Console.Write("Inserisci il cognome: ");
            string cognome = Console.ReadLine();

            DateTime dataNascita = ControlloDataNascita();

            string codiceFiscale = ControlloCodiceFiscale();

            char sesso = ControllaSesso();

            Console.Write("Inserisci il comune di residenza: ");
            string comuneResidenza = Console.ReadLine();

            double redditoAnnuale = ControlloRedditoAnnuale();


            Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuale);

            double impostaDaVersare = contribuente.CalcolaImposta();

            Console.WriteLine("==================================================");
            Console.WriteLine($"CALCOLO DELL'IMPOSTA DA VERSARE:\nContribuente: {nome} {cognome},\nnato il {dataNascita.ToString("dd/MM/yyyy")} ({sesso.ToString().ToUpper()}),\nresidente in {comuneResidenza},\ncodice fiscale: {codiceFiscale.ToUpper()}\nReddito dichiarato: € {redditoAnnuale:0.00}\nIMPOSTA DA VERSARE: € {impostaDaVersare:0.00}");
            Console.WriteLine("==================================================");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Si è verificato un errore: {ex.Message}");
        }

    }

    static DateTime ControlloDataNascita()
    {
        DateTime dataNascita;
        Console.Write("Inserisci la data di nascita (formato gg/mm/aaaa): ");
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dataNascita))
        {
            Console.Write("Data non valida. Inserisci la data di nascita (formato gg/mm/aaaa): ");
        }
        return dataNascita;
    }

    static string ControlloCodiceFiscale()
    {
        Console.Write("Inserisci il codice fiscale (16 caratteri): ");
        string codiceFiscale = Console.ReadLine();

        while (codiceFiscale.Length != 16)
        {
            Console.Write("Valore non valido. Inserisci il codice fiscale (16 caratteri): ");
            codiceFiscale = Console.ReadLine();
        }
        return codiceFiscale;
    }

    static char ControllaSesso()
    {
        Console.Write("Inserisci il sesso (M/F): ");

        string input;

        while (true)
        {
            input = Console.ReadLine().ToUpper();
            if (input == "M" || input == "F")
            {
                return input[0];
            }
            else
            {
                Console.Write("Valore non valido. Inserisci il sesso (M/F): ");
            }
        }
    }

    static double ControlloRedditoAnnuale()
    {
        double redditoAnnuale;
        Console.Write("Inserisci il reddito annuale: ");
        while (!double.TryParse(Console.ReadLine(), out redditoAnnuale) || redditoAnnuale < 0)
        {
            Console.Write("Valore non valido. Inserisci un numero positivo: ");
        }
        return redditoAnnuale;
    }

    class Contribuente
    {
        public string Nome { get; set; } = "";
        public string Cognome { get; set; } = "";
        public DateTime DataNascita { get; set; }
        public string CodiceFiscale { get; set; } = "";
        public char Sesso { get; set; }
        public string ComuneResidenza { get; set; } = "";
        public double RedditoAnnuale { get; set; }

        public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, char sesso, string comuneResidenza, double redditoAnnuale)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = codiceFiscale;
            Sesso = sesso;
            ComuneResidenza = comuneResidenza;
            RedditoAnnuale = redditoAnnuale;
        }

        public double CalcolaImposta()
        {
            if (RedditoAnnuale <= 15000)
            {
                return RedditoAnnuale * 0.23;
            }
            else if (RedditoAnnuale <= 28000)
            {
                return 3450 + (RedditoAnnuale - 15000) * 0.27;
            }
            else if (RedditoAnnuale <= 55000)
            {
                return 6960 + (RedditoAnnuale - 28000) * 0.38;
            }
            else if (RedditoAnnuale <= 75000)
            {
                return 17220 + (RedditoAnnuale - 55000) * 0.41;
            }
            else
            {
                return 25420 + (RedditoAnnuale - 75000) * 0.43;
            }
        }

    }
}