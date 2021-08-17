Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.Extensions.Configuration
Imports NorthWindCoreLibrary_vb.Models

Namespace Data
    Partial Public Class NorthWindContext
        Inherits DbContext

        Public Sub New()
        End Sub

        Public Sub New(options As DbContextOptions(Of NorthWindContext))
            MyBase.New(options)
        End Sub

        Public Overridable Property BusinessEntityPhones As DbSet(Of BusinessEntityPhone)
        Public Overridable Property Categories As DbSet(Of Category)
        Public Overridable Property Contacts As DbSet(Of Contact)
        Public Overridable Property ContactDevices As DbSet(Of ContactDevice)
        Public Overridable Property ContactTypes As DbSet(Of ContactType)
        Public Overridable Property Countries As DbSet(Of Country)
        Public Overridable Property Customers As DbSet(Of Customer)
        Public Overridable Property Employees As DbSet(Of Employee)
        Public Overridable Property EmployeeTerritories As DbSet(Of EmployeeTerritory)
        Public Overridable Property Orders As DbSet(Of Order)
        Public Overridable Property OrderDetails As DbSet(Of OrderDetail)
        Public Overridable Property PhoneTypes As DbSet(Of PhoneType)
        Public Overridable Property Products As DbSet(Of Product)
        Public Overridable Property Regions As DbSet(Of Region)
        Public Overridable Property Shippers As DbSet(Of Shipper)
        Public Overridable Property Suppliers As DbSet(Of Supplier)
        Public Overridable Property Territories As DbSet(Of Territory)

        Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
            If Not optionsBuilder.IsConfigured Then
                optionsBuilder.UseSqlServer(BuildConnection())
            End If
        End Sub
        Private Shared Function BuildConnection() As String

            Dim configuration = (New ConfigurationBuilder()).AddJsonFile("appsettings.json", True, True).Build()

            Dim sections = configuration.GetSection("database").GetChildren().ToList()

            Return $"Data Source={sections(1).Value};Initial Catalog={sections(0).Value};Integrated Security={sections(2).Value}"

        End Function
        Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")

            modelBuilder.Entity(Of BusinessEntityPhone)(
                Sub(entity)
                    entity.ToTable("BusinessEntityPhone")
                    entity.Property(Function(e) e.BusinessEntityPhoneId).HasColumnName("BusinessEntityPhoneID")
                    entity.Property(Function(e) e.ModifiedDate).HasDefaultValueSql("(getdate())")
                    entity.Property(Function(e) e.PhoneNumber).HasComment("Phone number")
                    entity.Property(Function(e) e.PhoneNumberTypeId).HasColumnName("PhoneNumberTypeID")
                End Sub)

            modelBuilder.Entity(Of Category)(
                Sub(entity)
                    entity.Property(Function(e) e.CategoryId).
                        HasColumnName("CategoryID").
                        HasComment("Primary key")
                    entity.Property(Function(e) e.CategoryName).
                        IsRequired().
                        HasMaxLength(15)
                    entity.Property(Function(e) e.Description).HasColumnType("ntext")
                    entity.Property(Function(e) e.Picture).HasColumnType("image")
                End Sub)

            modelBuilder.Entity(Of Contact)(
                Sub(entity)
                    entity.Property(Function(e) e.ContactId).HasComment("Id")
                    entity.Property(Function(e) e.ContactTypeIdentifier).HasComment("Contact Type Id")
                    entity.Property(Function(e) e.FirstName).HasComment("First name")
                    entity.Property(Function(e) e.LastName).HasComment("Last name")
                    entity.HasOne(Function(d) d.ContactTypeIdentifierNavigation).
                        WithMany(Function(p) p.Contacts).
                        HasForeignKey(Function(e) e.ContactTypeIdentifier).
                        HasConstraintName("FK_Contacts_ContactType")
                End Sub)

            modelBuilder.Entity(Of ContactDevice)(
                Sub(entity)
                    entity.Property(Function(e) e.Id).HasColumnName("id")
                    entity.HasOne(Function(d) d.Contact).
                        WithMany(Function(p) p.ContactDevices).
                        HasForeignKey(Function(e) e.ContactId).
                        HasConstraintName("FK_ContactDevices_Contacts1")
                    entity.HasOne(Function(d) d.PhoneTypeIdentifierNavigation).
                        WithMany(Function(p) p.ContactDevices).
                        HasForeignKey(Function(e) e.PhoneTypeIdentifier).
                        HasConstraintName("FK_ContactDevices_PhoneType")
                End Sub)

            modelBuilder.Entity(Of ContactType)(
                Sub(entity)
                    entity.HasKey(Function(e) e.ContactTypeIdentifier)
                    entity.ToTable("ContactType")
                End Sub)

            modelBuilder.Entity(Of Country)(
                Sub(entity)
                    entity.HasKey(Function(e) e.CountryIdentifier)
                End Sub)

            modelBuilder.Entity(Of Customer)(
                Sub(entity)
                    entity.HasKey(Function(e) e.CustomerIdentifier).
                        HasName("PK_Customers_1")
                    entity.HasIndex(Function(e) e.City, "City")
                    entity.HasIndex(Function(e) e.CompanyName, "CompanyName")
                    entity.HasIndex(Function(e) e.PostalCode, "PostalCode")
                    entity.HasIndex(Function(e) e.Region, "Region")
                    entity.Property(Function(e) e.CustomerIdentifier).HasComment("Id")
                    entity.Property(Function(e) e.City).
                        HasMaxLength(15).
                        HasComment("City")
                    entity.Property(Function(e) e.CompanyName).
                        IsRequired().
                        HasMaxLength(40).
                        HasComment("Company")
                    entity.Property(Function(e) e.ContactId).HasComment("ContactId")
                    entity.Property(Function(e) e.ContactTypeIdentifier).HasComment("ContactTypeIdentifier")
                    entity.Property(Function(e) e.CountryIdentifier).HasComment("CountryIdentifier")
                    entity.Property(Function(e) e.Fax).
                        HasMaxLength(24).
                        HasComment("Fax")
                    entity.Property(Function(e) e.ModifiedDate).
                        HasDefaultValueSql("(getdate())").
                        HasComment("Modified Date")
                    entity.Property(Function(e) e.Phone).
                        HasMaxLength(24).
                        HasComment("Phone")
                    entity.Property(Function(e) e.PostalCode).
                        HasMaxLength(10).
                        HasComment("Postal Code")
                    entity.Property(Function(e) e.Region).
                        HasMaxLength(15).
                        HasComment("Region")
                    entity.Property(Function(e) e.Street).
                        HasMaxLength(60).
                        HasComment("Street")
                    entity.HasOne(Function(d) d.Contact).
                        WithMany(Function(p) p.Customers).
                        HasForeignKey(Function(e) e.ContactId).
                        HasConstraintName("FK_Customers_Contacts")
                    entity.HasOne(Function(d) d.ContactTypeIdentifierNavigation).
                        WithMany(Function(p) p.Customers).
                        HasForeignKey(Function(e) e.ContactTypeIdentifier).
                        HasConstraintName("FK_Customers_ContactType")
                    entity.HasOne(Function(d) d.CountryIdentifierNavigation).
                        WithMany(Function(p) p.Customers).
                        HasForeignKey(Function(e) e.CountryIdentifier).
                        HasConstraintName("FK_Customers_Countries")
                End Sub)

            modelBuilder.Entity(Of Employee)(
                Sub(entity)
                    entity.Property(Function(e) e.EmployeeId).
                        HasColumnName("EmployeeID").
                        HasComment("Id")
                    entity.Property(Function(e) e.Address).
                        HasMaxLength(60).
                        HasComment("Street")
                    entity.Property(Function(e) e.BirthDate).
                        HasColumnType("datetime").
                        HasComment("Birth date")
                    entity.Property(Function(e) e.City).HasMaxLength(15)
                    entity.Property(Function(e) e.Extension).HasMaxLength(4)
                    entity.Property(Function(e) e.FirstName).
                        IsRequired().
                        HasMaxLength(10).
                        HasComment("First name")
                    entity.Property(Function(e) e.HireDate).
                        HasColumnType("datetime").
                        HasComment("Hiredate")
                    entity.Property(Function(e) e.HomePhone).
                        HasMaxLength(24).
                        HasComment("Home phone")
                    entity.Property(Function(e) e.LastName).
                        IsRequired().
                        HasMaxLength(20).
                        HasComment("Last name")
                    entity.Property(Function(e) e.PostalCode).HasMaxLength(10)
                    entity.Property(Function(e) e.Region).HasMaxLength(15)
                    entity.Property(Function(e) e.ReportsTo).HasComment("Manager")
                    entity.Property(Function(e) e.TitleOfCourtesy).
                        HasMaxLength(25).
                        HasComment("Title")
                    entity.HasOne(Function(d) d.ContactTypeIdentifierNavigation).
                        WithMany(Function(p) p.Employees).
                        HasForeignKey(Function(e) e.ContactTypeIdentifier).
                        HasConstraintName("FK_Employees_ContactType")
                    entity.HasOne(Function(d) d.CountryIdentifierNavigation).
                        WithMany(Function(p) p.Employees).
                        HasForeignKey(Function(e) e.CountryIdentifier).
                        HasConstraintName("FK_Employees_Countries")
                End Sub)

            modelBuilder.Entity(Of EmployeeTerritory)(
                Sub(entity)
                    entity.HasKey(Function(e) New With {e.EmployeeId, e.TerritoryId}).
                        IsClustered(False)
                    entity.Property(Function(e) e.EmployeeId).HasColumnName("EmployeeID")
                    entity.Property(Function(e) e.TerritoryId).
                        HasMaxLength(20).
                        HasColumnName("TerritoryID")
                    entity.HasOne(Function(d) d.Employee).
                        WithMany(Function(p) p.EmployeeTerritories).
                        HasForeignKey(Function(e) e.EmployeeId).
                        OnDelete(DeleteBehavior.ClientSetNull).
                        HasConstraintName("FK_EmployeeTerritories_Employees")
                    entity.HasOne(Function(d) d.Territory).
                        WithMany(Function(p) p.EmployeeTerritories).
                        HasForeignKey(Function(e) e.TerritoryId).
                        OnDelete(DeleteBehavior.ClientSetNull).
                        HasConstraintName("FK_EmployeeTerritories_Territories")
                End Sub)

            modelBuilder.Entity(Of Order)(
                Sub(entity)
                    entity.Property(Function(e) e.OrderId).HasColumnName("OrderID")
                    entity.Property(Function(e) e.EmployeeId).HasColumnName("EmployeeID")
                    entity.Property(Function(e) e.Freight).HasColumnType("money")
                    entity.Property(Function(e) e.OrderDate).HasColumnType("datetime")
                    entity.Property(Function(e) e.RequiredDate).HasColumnType("datetime")
                    entity.Property(Function(e) e.ShipAddress).HasMaxLength(60)
                    entity.Property(Function(e) e.ShipCity).HasMaxLength(15)
                    entity.Property(Function(e) e.ShipCountry).HasMaxLength(15)
                    entity.Property(Function(e) e.ShipPostalCode).HasMaxLength(10)
                    entity.Property(Function(e) e.ShipRegion).HasMaxLength(15)
                    entity.Property(Function(e) e.ShippedDate).HasColumnType("datetime")
                    entity.HasOne(Function(d) d.CustomerIdentifierNavigation).
                        WithMany(Function(p) p.Orders).
                        HasForeignKey(Function(e) e.CustomerIdentifier).
                        OnDelete(DeleteBehavior.Cascade).
                        HasConstraintName("FK_Orders_Customers2")
                    entity.HasOne(Function(d) d.Employee).
                        WithMany(Function(p) p.Orders).
                        HasForeignKey(Function(e) e.EmployeeId).
                        HasConstraintName("FK_Orders_Employees")
                    entity.HasOne(Function(d) d.ShipViaNavigation).
                        WithMany(Function(p) p.Orders).
                        HasForeignKey(Function(e) e.ShipVia).
                        HasConstraintName("FK_Orders_Shippers")
                End Sub)

            modelBuilder.Entity(Of OrderDetail)(
                Sub(entity)
                    entity.HasKey(Function(e) New With {e.OrderId, e.ProductId}).
                        HasName("PK_Order_Details")
                    entity.Property(Function(e) e.OrderId).HasColumnName("OrderID")
                    entity.Property(Function(e) e.ProductId).HasColumnName("ProductID")
                    entity.Property(Function(e) e.UnitPrice).HasColumnType("money")
                    entity.HasOne(Function(d) d.Order).
                        WithMany(Function(p) p.OrderDetails).
                        HasForeignKey(Function(e) e.OrderId).
                        HasConstraintName("FK_Order_Details_Orders")
                    entity.HasOne(Function(d) d.Product).
                        WithMany(Function(p) p.OrderDetails).
                        HasForeignKey(Function(e) e.ProductId).
                        OnDelete(DeleteBehavior.ClientSetNull).
                        HasConstraintName("FK_Order_Details_Products")
                End Sub)

            modelBuilder.Entity(Of PhoneType)(
                Sub(entity)
                    entity.HasKey(Function(e) e.PhoneTypeIdenitfier)
                    entity.ToTable("PhoneType")
                End Sub)

            modelBuilder.Entity(Of Product)(
                Sub(entity)
                    entity.Property(Function(e) e.ProductId).HasColumnName("ProductID")
                    entity.Property(Function(e) e.CategoryId).HasColumnName("CategoryID")
                    entity.Property(Function(e) e.ProductName).
                        IsRequired().
                        HasMaxLength(40)
                    entity.Property(Function(e) e.QuantityPerUnit).HasMaxLength(20)
                    entity.Property(Function(e) e.SupplierId).HasColumnName("SupplierID")
                    entity.Property(Function(e) e.UnitPrice).HasColumnType("money")
                    entity.HasOne(Function(d) d.Category).
                        WithMany(Function(p) p.Products).
                        HasForeignKey(Function(e) e.CategoryId).
                        HasConstraintName("FK_Products_Categories")
                    entity.HasOne(Function(d) d.Supplier).
                        WithMany(Function(p) p.Products).
                        HasForeignKey(Function(e) e.SupplierId).
                        HasConstraintName("FK_Products_Suppliers")
                End Sub)

            modelBuilder.Entity(Of Region)(
                Sub(entity)
                    entity.HasKey(Function(e) e.RegionId).
                        IsClustered(False)
                    entity.ToTable("Region")
                    entity.Property(Function(e) e.RegionId).
                        ValueGeneratedNever().
                        HasColumnName("RegionID")
                    entity.Property(Function(e) e.RegionDescription).
                        IsRequired().
                        HasMaxLength(50).
                        IsFixedLength(True)
                End Sub)

            modelBuilder.Entity(Of Shipper)(
                Sub(entity)
                    entity.Property(Function(e) e.ShipperId).
                        ValueGeneratedNever().
                        HasColumnName("ShipperID")
                    entity.Property(Function(e) e.CompanyName).
                        IsRequired().
                        HasMaxLength(40)
                    entity.Property(Function(e) e.Phone).HasMaxLength(24)
                End Sub)

            modelBuilder.Entity(Of Supplier)(
                Sub(entity)
                    entity.Property(Function(e) e.SupplierId).HasColumnName("SupplierID")
                    entity.Property(Function(e) e.City).HasMaxLength(15)
                    entity.Property(Function(e) e.CompanyName).
                        IsRequired().
                        HasMaxLength(40)
                    entity.Property(Function(e) e.ContactName).HasMaxLength(30)
                    entity.Property(Function(e) e.ContactTitle).HasMaxLength(30)
                    entity.Property(Function(e) e.Fax).HasMaxLength(24)
                    entity.Property(Function(e) e.HomePage).HasColumnType("ntext")
                    entity.Property(Function(e) e.Phone).HasMaxLength(24)
                    entity.Property(Function(e) e.PostalCode).HasMaxLength(10)
                    entity.Property(Function(e) e.Region).HasMaxLength(15)
                    entity.Property(Function(e) e.Street).HasMaxLength(60)
                    entity.HasOne(Function(d) d.CountryIdentifierNavigation).
                        WithMany(Function(p) p.Suppliers).
                        HasForeignKey(Function(e) e.CountryIdentifier).
                        HasConstraintName("FK_Suppliers_Countries")
                End Sub)

            modelBuilder.Entity(Of Territory)(
                Sub(entity)
                    entity.HasKey(Function(e) e.TerritoryId).
                        IsClustered(False)
                    entity.Property(Function(e) e.TerritoryId).
                        HasMaxLength(20).
                        HasColumnName("TerritoryID")
                    entity.Property(Function(e) e.RegionId).HasColumnName("RegionID")
                    entity.Property(Function(e) e.TerritoryDescription).
                        IsRequired().
                        HasMaxLength(50).
                        IsFixedLength(True)
                    entity.HasOne(Function(d) d.Region).
                        WithMany(Function(p) p.Territories).
                        HasForeignKey(Function(e) e.RegionId).
                        OnDelete(DeleteBehavior.ClientSetNull).
                        HasConstraintName("FK_Territories_Region")
                End Sub)

            OnModelCreatingPartial(modelBuilder)
        End Sub

        Partial Private Sub OnModelCreatingPartial(modelBuilder As ModelBuilder)
        End Sub
    End Class
End Namespace
