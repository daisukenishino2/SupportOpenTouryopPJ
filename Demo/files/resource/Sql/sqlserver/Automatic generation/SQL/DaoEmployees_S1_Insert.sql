﻿-- DaoEmployees_S1_Insert
-- 2014/2/9 日立 太郎
INSERT INTO 
  [Employees]
    (
      [EmployeeID],
      [LastName],
      [FirstName],
      [Title],
      [TitleOfCourtesy],
      [BirthDate],
      [HireDate],
      [Address],
      [City],
      [Region],
      [PostalCode],
      [Country],
      [HomePhone],
      [Extension],
      [Photo],
      [Notes],
      [ReportsTo],
      [PhotoPath]
    )
VALUES
    (
      @EmployeeID,
      @LastName,
      @FirstName,
      @Title,
      @TitleOfCourtesy,
      @BirthDate,
      @HireDate,
      @Address,
      @City,
      @Region,
      @PostalCode,
      @Country,
      @HomePhone,
      @Extension,
      @Photo,
      @Notes,
      @ReportsTo,
      @PhotoPath
    )
