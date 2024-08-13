// Абстрактный класс периферийного устройства
public abstract class Device
{
    public string SerialNumber { get; set; }
    public string Brand { get; set; }
    public int Cost { get; set; }

    public abstract void GetInfo();
}

// Класс наушников
public class Headphones : Device
{
    public string DesignType { get; set; }
    public string MountingMethod { get; set; }

    public override void GetInfo()
    {
        Console.WriteLine($"Headphones - Serial Number: {SerialNumber}, Brand: {Brand}, Cost: ${Cost}, Design Type: {DesignType}, Mounting Method: {MountingMethod}");
    }
}

// Класс микрофона
public class Microphone : Device
{
    public string FrequencyRange { get; set; }
    public string Sensitivity { get; set; }

    public override void GetInfo()
    {
        Console.WriteLine($"Microphone - Serial Number: {SerialNumber}, Brand: {Brand}, Cost: ${Cost}, Frequency Range: {FrequencyRange}, Sensitivity: {Sensitivity}");
    }
}

// Класс клавиатуры
public class Keyboard : Device
{
    public string Type { get; set; }
    public string Connectivity { get; set; }

    public override void GetInfo()
    {
        Console.WriteLine($"Keyboard - Serial Number: {SerialNumber}, Brand: {Brand}, Cost: ${Cost}, Type: {Type}, Connectivity: {Connectivity}");
    }
}

// Фабричный метод для создания периферийных устройств
public abstract class DeviceFactory
{
    protected abstract Device CreateDevice();

    public Device OrderDevice(string serialNumber, string brand, int cost)
    {
        Device device = CreateDevice();
        device.SerialNumber = serialNumber;
        device.Brand = brand;
        device.Cost = cost;
        return device;
    }
}

// Конкретная фабрика для наушников
public class HeadphonesFactory : DeviceFactory
{
    protected override Device CreateDevice()
    {
        return new Headphones();
    }
}

// Конкретная фабрика для микрофона
public class MicrophoneFactory : DeviceFactory
{
    protected override Device CreateDevice()
    {
        return new Microphone();
    }
}

// Конкретная фабрика для клавиатуры
public class KeyboardFactory : DeviceFactory
{
    protected override Device CreateDevice()
    {
        return new Keyboard();
    }
}

class Program
{
    static void Serial_number_once(List<Device> devices, Device this_device)
    {
        if (devices.Count > 0)
        {
            foreach (Device device in devices)
            {
                if (device.SerialNumber == this_device.SerialNumber)
                {
                    Console.WriteLine("Device with this serial number exists\n");
                    break;
                }
                else
                {
                    devices.Add(this_device);
                    break;
                }
            }
        }
        else
        {
            devices.Add(this_device);
        }
    }


    static void Main(string[] args)
    {
        List<Device> devices = new List<Device>();

        DeviceFactory headphonesFactory = new HeadphonesFactory();
        Device headphones = headphonesFactory.OrderDevice("123456", "Sony", 100);
        ((Headphones)headphones).DesignType = "Over-ear";
        ((Headphones)headphones).MountingMethod = "Wireless";
        Serial_number_once(devices, headphones);

        DeviceFactory microphoneFactory = new MicrophoneFactory();
        Device microphone = microphoneFactory.OrderDevice("654321", "Shure", 150);
        ((Microphone)microphone).FrequencyRange = "20Hz - 20kHz";
        ((Microphone)microphone).Sensitivity = "37 mV/Pa";
        Serial_number_once(devices, microphone);

        DeviceFactory keyboardFactory = new KeyboardFactory();
        Device keyboard = keyboardFactory.OrderDevice("987654", "Logitech", 80);
        ((Keyboard)keyboard).Type = "Mechanical";
        ((Keyboard)keyboard).Connectivity = "Wired";
        Serial_number_once(devices, keyboard);

        Device headphones2 = headphonesFactory.OrderDevice("123456", "Louis Vuitton", 480);
        ((Headphones)headphones2).DesignType = "plugged-in";
        ((Headphones)headphones2).MountingMethod = "Wired";
        Serial_number_once(devices, headphones2);


        Console.WriteLine("List of Devices:");
        foreach (Device device in devices)
        {
            device.GetInfo();
        }

        Console.WriteLine();
        while (true)
        {
            Console.WriteLine("Information of a Specific Device:");
            Console.WriteLine(@"Choose a parametr:
            1 - Serial number
            2 - Brand
            3 - Cost");
            string param = Console.ReadLine();
            switch (param)
            {
                case "1":
                    Console.WriteLine("Type serial number: ");
                    string num = Console.ReadLine();
                    foreach (Device device in devices)
                    {
                        if (device.SerialNumber == num)
                        {
                            device.GetInfo();
                        }
                        else { Console.WriteLine("No such device"); break; }
                    }
                    break;

                case "2":
                    Console.WriteLine("Type brand: ");
                    string brand = Console.ReadLine();
                    foreach (Device device in devices)
                    {
                        if (device.Brand == brand)
                        {
                            device.GetInfo();
                        }
                        
                    }
                    break;
                        
                case "3":
                    Console.Write("Type cost: ");
                    int cost = Convert.ToInt32(Console.ReadLine());
                    foreach (Device device in devices)
                    {
                        if (device.Cost == cost)
                        {
                            device.GetInfo();
                        }
                       
                    }
                    break;

                default: Console.WriteLine("Incorrect!\n");
                    break;
            }
        }
    }
}