# TechieByNight

TechieByNight is an ASP.NET MVC web application that serves as a backend example project. It provides functionalities such as user authentication, blog management, tag management, and user management. The project utilizes SQL Server for the database, Razor Pages for server-side rendering, and Bootstrap for frontend styling. Additionally, it integrates Cloudinary API for image upload and Froala WYSIWYG editor for content creation.

## Features

- **User Authentication:** Includes login and registration functionality to authenticate users.
- **Blog Management:** Allows users to publish blogs with rich text content using Froala editor.
- **Tag Management:** Provides the ability to add tags to blogs for organization and categorization.
- **User Management:** Admin functionality for managing user accounts, including roles and permissions.
- **Image Upload:** Integration with Cloudinary API for seamless image uploading and management.

## Getting Started

To get started with TechieByNight, follow these steps:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/MahdiNoorzadeh/TechieByNight.git
   ```

2. **Set up the database:**
   - Install SQL Server and create a database for the project.
   - Update the connection string in `appsettings.json` to point to your SQL Server database.

3. **Set up Cloudinary account:**
   - Sign up for a Cloudinary account at [https://cloudinary.com](https://cloudinary.com).
   - Obtain your Cloudinary API credentials (cloud name, API key, API secret).

4. **Configure Cloudinary integration:**
   - Replace the placeholders for Cloudinary configuration in `appsettings.json` with your actual Cloudinary API credentials.

5. **Install dependencies:**
   - Open the project in Visual Studio or your preferred IDE.
   - Restore NuGet packages and npm packages.

6. **Run the application:**
   - Build and run the project in your IDE.
   - The application should launch in your default web browser.

7. **Explore the functionalities:**
   - Navigate through the login and registration pages to authenticate.
   - Explore the blog management, tag management, and user management functionalities.

## Credits

This project was inspired by [https://www.udemy.com/course/aspnet-core-razor-pages-web-application-development/?couponCode=ST7MT41824#instructor-1]. Special thanks to [Sameer Saini] for providing guidance and resources.

## License

This project is licensed under the [MIT License](LICENSE).
