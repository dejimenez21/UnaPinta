# Getting Started
To ger a local copy up and running follow these steps.

## Prerequisites

* dotnet core sdk 3.1

## Installation

1. Clone the repo

    > git clone https://github.com/dejimenez21/UnaPinta/tree/master.git

2. Set the SECRET Enviroment variable with any value
    > setx SECRET "UnaPintaSecretKey"

3. Set the database connection you want to use. 
    > In the **ConectionType** section of the **appsettings.Development.json** file in the **UnaPinta.Api** project:
    > * If you want to use the **Azure DB** connection set **IsAzureConnection** to **true**.
    > * If you want to use a **Local SQL DB** connection set **IsAzureConnection** to **false** and **IsLocalConnection** to **true**. Also, you will need to set the **LocalConnection** property in the **ConnectionStrings** section to the connection string of your **Local DB**.
    > * If you want to use the **SQLite DB** file in the root of the repository, set both **IsAzureConnection** and **IsLocalConnection** to **false**.

4. **(Optional)** Set the browser to launch the Swagger API documentation page when running the project.
    > In the **profiles** section of the **launchSettings.json** file in the **UnaPinta.Api** project, set the **launchBrowser** property to  **true** in the **IIS Express** object.

5. Set the project to run with **IIS Express**.


