namespace OOP_5
{

    #region Part1: Theoritical Questions
    //Part1: Theoritical
    //Question1:
    //a)The value of the first object variable is copied into the other as a value not as a refernce address
    //b)No, It creates a new refernce that refers to the same object in heap but it doesnt create a new object
    //c)Copying an object is copying its value and assigning it to another object variable while copying its reference is copying its address to the other object variable
    //Question2:
    //a)A Shallow copy is a copy of the object that shares references to the same objects in memory and the copy and the original are not independent(REFERENCE TYPES)
    //b)A deep copy creates a new object and copies all the values and references to new objects in memory and the copy and the original are completely independent
    //c)When a shallow copy is made, changes made to the original object will affect the copy and vice versa. 
    //d)When a deep copy is made, changes made to the original object will not affect the copy and vice versa.
    //e) when you need to modify nested mutable data inside a cloned structure without accidentally changing the values in the original data source
    #endregion

    #region Part2: Practical Questions

    #region DeliveryAdress Class
    public struct DeliveryAddress
        {
            string City;
            string Street;
            int BuildingNumber;
            public DeliveryAddress(string city, string street, int buildingNumber)
            {
                City = city;
                Street = street;
                BuildingNumber = buildingNumber;
            }
            public DeliveryAddress(string street)
            {
                Street = street;
                City = "New York";
                BuildingNumber = 1;
            }
            public string GetFullAddress()
            {
                return $"{BuildingNumber} {Street} ,{City}";
            }


        }
        #endregion

        #region Shipment Class (Parent Class)
        //Shipment Class(Parent Class)
        public class Shipment
        {
            string trackingCode;
            string description;
            int weight;
            decimal deliveryFee;
            DeliveryAddress destination;


            //Properties
            //1.Tracking Code Property
            public string TrackingCode
            {
                get
                {
                    return trackingCode;
                }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        trackingCode = value;
                    }
                    else
                    {
                        throw new ArgumentException("Tracking Code can't be null or empty");
                    }
                }
            }
            //2.Description Property
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Description cannot be null or empty.");
                    }
                    else
                    {
                        description = value;
                    }
                }
            }
            //3.Weight Property
            public int Weight
            {
                get
                {
                    return weight;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("Weight must be a positive number.");
                    }
                    weight = value;
                }

            }
            //4.Delivery Fee Property
            public decimal DeliveryFee
            {
                get
                {
                    return deliveryFee;
                }
                private set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("Delivery fee must be a positive number.");
                    }
                    else
                    {
                        deliveryFee = value;
                    }
                }
            }
            //5.Destination Property
            public DeliveryAddress Destination
            {
                get
                {
                    return destination;
                }
                set
                {
                    destination = value;
                }
            }
            public virtual decimal EstimatedCost
            {
                get
                {
                    return deliveryFee + (weight * 5);
                }
            }

            //////////////Constructors
            //1st Constructor
            public Shipment(string trackingCode)
            {
                this.trackingCode = trackingCode;
                description = "Unknown";
                weight = 1;
                deliveryFee = 50;
                destination = new DeliveryAddress("Nasr city", "Al Nahas", 15);
            }
            //2nd Constructor
            public Shipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee)
            {
                trackingCode = TrackingCode;
                description = Description;
                weight = Weight;
                deliveryFee = DeliveryFee;
            }
            //////////////Methods
            //Update DeliveryFee Method
            public void UpdateDeilveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    deliveryFee = newFee;
                }
                else
                {
                    throw new ArgumentException("Delivery fee must be a positive number.");
                }
            }
            public void UpdateDeilveryFee(decimal newFee, int packingWeight)
            {
                if (newFee > 0)
                {
                    deliveryFee = newFee + (packingWeight * 5);
                }
                else
                {
                    throw new ArgumentException("Delivery fee must be a positive number.");
                }
            }
            //Print Shipment Method
            public virtual string PrintShipmentDetails()
            {
                return $"Tracking Code: {TrackingCode}\n";
            }
            //Copy shipment method
            public Shipment CopyShipment()
            {
            return new Shipment(this.trackingCode, this.description, this.weight, this.deliveryFee);
            }
    }
    #endregion

        #region Standard Shipment Class(Child Class)
    //Standard Shipment Class(Child Class)
    public class StandardShipment : Shipment, ITrackable, IInsurable
        {
            //Chaining Constructor
            public StandardShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee) : base(TrackingCode, Description, Weight, DeliveryFee)
            {
            }
            //Print Shipment Override method
            public override string PrintShipmentDetails()
            {
                return "Standard Shipment\n\n" + base.PrintShipmentDetails() + $"Description: {Description}\n" + $"Estimated Cost: {EstimatedCost}\n";
            }
            //ITrackable Implementation
            public string GetTrackingStatus()
            {
                return $"Tracking Status\nShipment {TrackingCode} is Ready.\n";
            }
            //IInsurable Implementation
            public decimal CalculateInsurance()
            {
                return EstimatedCost * 0.05m;
            }
        }
        #endregion

        #region Express Shipment Class(Child Class)
        //Express Shipment Class(Child Class)
        public class ExpressShipment : Shipment, ITrackable, IInsurable
        {
            decimal extrafee;
            //ExtraFee Property
            public decimal ExtraFee
            {
                get
                {
                    return extrafee;
                }
                set
                {
                    if (value < 0)
                        throw new ArgumentException("Extra fee must be greater than or equal to 0");
                    else
                        extrafee = value;
                }
            }
            //Override Estimated cost property
            public override decimal EstimatedCost
            {
                get
                {
                    return base.EstimatedCost + extrafee;
                }

            }
            //Print Shipment Override method
            public override string PrintShipmentDetails()
            {
                return "Express Shipment\n\n" + base.PrintShipmentDetails() + $"Extra Fee: {extrafee}\n" + $"Estimated Cost: {EstimatedCost}";
            }
            public ExpressShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee, decimal extrafee) : base(TrackingCode, Description, Weight, DeliveryFee)
            {
                this.ExtraFee = extrafee;
            }
            //ITrackable Implementation
            public string GetTrackingStatus()
            {
                return $"Shipment {TrackingCode} is Out for Delivery.\n";
            }
            //IInsurable Implementation
            public decimal CalculateInsurance()
            {
                return EstimatedCost * 0.08m;
            }
        }
        #endregion

        #region International Shipment Class(Child Class) 
        //International Shipment Class(parent class)
        public class InternationalShipment : Shipment, ITrackable, IInsurable
        {
            string destinationcountry;
            decimal customsfee;
            //Destination Country property
            public string DestinationCountry
            {
                get
                {
                    return destinationcountry;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Description cannot be null or empty.");
                    }
                    else
                    {
                        destinationcountry = value;
                    }


                }
            }
            //Customs Fee property
            public decimal CustomsFee
            {
                get
                {
                    return customsfee;
                }
                set
                {
                    if (value < 0)
                        throw new ArgumentException("Extra fee must be greater than or equal to 0");
                    else
                        customsfee = value;
                }
            }
            //Override Estimated cost property
            public override decimal EstimatedCost
            {
                get
                {
                    return base.EstimatedCost + customsfee;
                }

            }
            //Print Shipment Override method
            public override string PrintShipmentDetails()
            {
                return "International Shipment\n\n" + base.PrintShipmentDetails() + $"Destination: {destinationcountry}\n" + $"Estimated Cost: {EstimatedCost}";
            }
            //Constructor chaining
            public InternationalShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee, decimal customsfee, string destinationcountry) : base(TrackingCode, Description, Weight, DeliveryFee)
            {
                this.CustomsFee = customsfee;
                this.DestinationCountry = destinationcountry;
            }
            //ITrackable Implementation
            public string GetTrackingStatus()
            {
                return $"Shipment {TrackingCode} has been delivered.";
            }
            //IInsurable Implementation
            public decimal CalculateInsurance()
            {
                return EstimatedCost * 0.12m;
            }
        }
        #endregion

        #region Delivery Center Class
        //Delivery Center Class
        public class DeliveryCenter
        {
            public string CenterName;
            public string AssignedDriver;
            Shipment[] shipment;
            public DeliveryCenter()
            {
                shipment = new Shipment[20];
            }
            //Integer Indexer
            public Shipment this[int index]
            {
                get
                {
                    if (index >= 0 && index < shipment.Length)
                    {
                        return shipment[index];
                    }
                    return default;
                }
                set
                {
                    if (index >= 0 && index < shipment.Length)
                    {
                        shipment[index] = value;
                    }
                }
            }
            //String Indexer
            public Shipment this[string trackingcode]
            {
                get
                {
                    for (int i = 0; i < shipment.Length; i++)
                    {
                        if (shipment[i] != null &&
                            shipment[i].TrackingCode == trackingcode)
                        {
                            return shipment[i];
                        }
                    }

                    return null;
                }
            }
            //AddShipment Method
            public bool AddShipment(Shipment newshipment)
            {
                for (int i = 0; i < shipment.Length; i++)
                {
                    if (shipment[i] == null)
                    {
                        shipment[i] = newshipment;
                        return true;
                    }
                }

                return false;
            }
            //RemoveShipment Method
            public bool RemoveShipment(string trackingcode)
            {
                for (int i = 0; i < shipment.Length; i++)
                {
                    if (shipment[i].TrackingCode != null && shipment[i].TrackingCode == trackingcode)
                    {
                        shipment[i] = null;
                        return true;
                    }
                }
                return false;
            }
            //Print all shipments method
            public void PrintAllShipments()
            {
                foreach (Shipment s in shipment)
                {
                    if (s != null)
                    {
                        Console.WriteLine(s.PrintShipmentDetails());
                    }
                }
            }
            //PrintShipment(ITrackable shipment)
            public void PrintShipment(ITrackable shipment)
            {
                if (shipment != null)
                {
                    Console.WriteLine(shipment.GetTrackingStatus());
                }
            }
            //PrintShipment(IInsurable shipment)
            public void PrintShipment(IInsurable shipment)
            {
                if (shipment != null)
                {
                    Console.WriteLine($"Insurance\n {shipment.GetType().Name}: {shipment.CalculateInsurance()}");
                }
            }
            public void PrintTrackingStatuses()
            {

                foreach (ITrackable t in shipment)
                {
                    if (t != null)
                    {
                        t.GetTrackingStatus();
                    }
                }
            }
            public void PrintInsuranceCosts()
            {
                foreach (IInsurable i in shipment)
                {
                    if (i != null)
                    {
                        i.CalculateInsurance();
                    }
                }
            }

        }
        #endregion

        #region Delivery Helper Class
        public static class DeliveryHelper
        {
            public static void PrintShipmentDetails(Shipment shipment)
            {
                if (shipment != null)
                    Console.WriteLine(shipment.PrintShipmentDetails());
            }
        }
        #endregion

        #region Driver Class
        public class Driver
        {
            public string name
            {
                get;
                set
                {
                    if (value is null || value is "")
                        throw new ArgumentException("Please enter a valid driver's name");
                }
            }

            public Driver(string name)
            {
                this.name = name;
            }

        }
        #endregion


        #region Main
        internal class Program
        {
            static void Main(string[] args)
            {
                ////a.Create a Driver
                //Console.WriteLine("Enter Driver Name:");
                //string driverName = Console.ReadLine();

                //Driver driver = new Driver(driverName);
                //// b. Create a DeliveryCenter
                //Console.WriteLine("Enter Delivery Center Name:");
                //string centerName = Console.ReadLine();

                //DeliveryCenter center = new DeliveryCenter();
                //center.CenterName = centerName;

                ////c.Assign driver to delivery center
                //center.AssignedDriver = driver.name;
                // a. Create Standard Shipment
                //StandardShipment standardShipment =
                //    new StandardShipment(
                //        "SH001",
                //        "Laptop",
                //        2,
                //        100.0m
                //    );

                //// b. Create Express Shipment
                //ExpressShipment expressShipment =
                //   new ExpressShipment(
                //     "SH002",
                //     "Laptop",
                //     2,
                //     100.0m,
                //     30.0m
                //   );
                //// c. Create International Shipment
                //InternationalShipment internationalShipment =
                //    new InternationalShipment(
                //      "SH003",
                //      "Laptop",
                //       2,
                //       100.0m,
                //       30.0m,
                //      "Germany"
                //    );

                //// d. Add shipments to Delivery Center
                //DeliveryCenter center = new DeliveryCenter();
                //center.AddShipment(standardShipment);
                //center.AddShipment(expressShipment);
                //center.AddShipment(internationalShipment);
                ////e. Print all shiipment details
                //for (int i = 0; i < 20; i++)
                //{
                //    if (center[i] != null)
                //    {
                //        DeliveryHelper.PrintShipmentDetails(center[i]);
                //        Console.WriteLine("------------------------------------");
                //    }
                //}
                ////f. Print tracking status for each shipment
                //for (int i = 0; i < 20; i++)
                //{
                //    if (center[i] != null)
                //    {
                //        center.PrintShipment(center[i] as ITrackable);

                //    }
                //}
                ////f. Print tracking status for each shipment
                //for (int i = 0; i < 20; i++)
                //{
                //    if (center[i] != null)
                //    {
                //        center.PrintShipment(center[i] as IInsurable);

                //    }
                //}
                //ITrackable[] trackableShipments = new ITrackable[] { standardShipment, expressShipment, internationalShipment };
                //for (int i = 0; i < trackableShipments.Length; i++)
                //{
                //    if (trackableShipments[i] != null)
                //    {

                //        Console.WriteLine(trackableShipments[i].GetTrackingStatus());
                //    }
                //}
                //IInsurable[] insurableShipments = new IInsurable[] { standardShipment, expressShipment, internationalShipment };
                //for (int i = 0; i < insurableShipments.Length; i++)
                //{
                //    if (insurableShipments[i] != null)
                //    {
                //        Console.WriteLine(insurableShipments[i].CalculateInsurance());

                //    }
                //}



                //// h & i. Print all shipments 
                //Console.WriteLine("------------------------------------");
                //Console.WriteLine($"Delivery Center: {center.CenterName}");
                //Console.WriteLine("------------------------------------");
                //Console.WriteLine($"\nDriver Name: {driver.name}");
                //Console.WriteLine("------------------------------------");
                //center.PrintAllShipments();

                ////j. demonstrate both versions of update weight
                //int updatedWeight = 5;
                //standardShipment.UpdateDeilveryFee(20.5m);
                //Console.WriteLine("------------------------------------");
                //Console.WriteLine($"Original Weight : {expressShipment.Weight} KG");
                //expressShipment.Weight = updatedWeight;
                //expressShipment.UpdateDeilveryFee(50.0m, updatedWeight);
                //Console.WriteLine($"Updated Weight : {expressShipment.Weight} KG");
                //Console.WriteLine($"Updated Weight after Packing : {expressShipment.Weight + 0.5} KG");

                //k. Build a Shipment[] holding mixed types and print all of them in a loop.
                //    Shipment[] mixedShipments = new Shipment[]
                //{
                //new StandardShipment("STD-101", "Textbooks", 3, 45.0m),
                //new ExpressShipment("EXP-202", "Smartphone", 1, 80.0m, 25.0m),
                //new InternationalShipment("INT-303", "Machine Parts", 12, 200.0m, 75.0m, "Canada")
                //};
                //    Console.WriteLine("========== MIXED SHIPMENTS ==========");
                //    foreach (Shipment s in mixedShipments)
                //    {
                //        if (s != null)
                //        {
                //            Console.WriteLine(s.PrintShipmentDetails());
                //            Console.WriteLine("------------------------------------------");
                //        }
                //    }

                //// 7. Search using the tracking-code indexer
                //Console.WriteLine("\nEnter Tracking Code to Search:");
                //string searchCode = Console.ReadLine();

                //Shipment foundShipment = center[searchCode];

                //if (foundShipment != null)
                //{
                //    Console.WriteLine("\nShipment Found:");
                //    Console.WriteLine(foundShipment.PrintShipmentDetails());
                //}
                //else
                //{
                //    Console.WriteLine("No Shipment Found!!");
                //}


                //// 8. Remove shipment
                //Console.WriteLine("\nEnter Tracking Code to Remove:");
                //string removeCode = Console.ReadLine();

                //bool removed = center.RemoveShipment(removeCode);

                //if (removed)
                //{
                //    Console.WriteLine("Shipment Removed Successfully.");
                //}
                //else
                //{
                //    Console.WriteLine("Shipment Not Found.");
                //}


                //// 9. Print remaining shipments
                //Console.WriteLine("\n========== REMAINING SHIPMENTS ==========");
                //center.PrintAllShipments();
                Shipment shipment1 = new StandardShipment("STD-001", "Books", 2, 50.0m);
                //create a copy of the shipment using the CopyShipment method
                Shipment shipment2 = shipment1.CopyShipment();
                Console.WriteLine($"Original Shipment: {shipment1.PrintShipmentDetails()}");
                Console.WriteLine($"Copied Shipment: {shipment2.PrintShipmentDetails()}");
                shipment2.TrackingCode = "STD-002";
                Console.WriteLine($"After changing the tracking code of the copied shipment: {shipment2.TrackingCode}");
                Console.WriteLine($"After changing the tracking code of the copied shipment: {shipment1.TrackingCode}");



        }
    }
        #endregion

        #endregion


    







}
