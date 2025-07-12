namespace LicenseManager;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Usage: LicenseManager.exe <master key> <hardware id> <license days> <output path>");
            return;
        }

        string mKey = args[0];
        string hardwareID = args[1];
        int licenseDays = int.Parse(args[2]);
        string outputPath = args[3];


        if (!Directory.Exists($@"{outputPath}"))
        {
            Directory.CreateDirectory($@"{outputPath}");
        }

        string licenseFile = $@"{outputPath}/hb.license";
        LicenseGenerator licenseGenerator = new();
        licenseGenerator.LoadMasterKeyFromString(mKey);
        licenseGenerator.Expiration_Date_Enabled = true;
        licenseGenerator.ExpirationDate = DateTime.Now.AddDays(licenseDays);
        licenseGenerator.Hardware_Enabled = true;
        licenseGenerator.HardwareID_Board = true;
        licenseGenerator.HardwareID_CPU = true;
        licenseGenerator.HardwareID_MAC = true;
        licenseGenerator.HardwareID = hardwareID;
        licenseGenerator.CreateLicenseFile(licenseFile);
        Console.WriteLine("create License Done");

        licenseGenerator = new();
        licenseGenerator.LoadMasterKeyFromString(mKey);
        licenseGenerator.LoadLicenseFromFile(licenseFile);
        Console.WriteLine(
            $"Hardware_Enabled：{licenseGenerator.Hardware_Enabled}, Expiration_Date_Enabled：{licenseGenerator.Expiration_Date_Enabled}, ExpirationDate：{licenseGenerator.ExpirationDate}");
    }
}