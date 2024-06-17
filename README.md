# Fourstrides Prototype Client Repository

Welcome to the Fourstrides company repository. This repository is home to our prototype client built with next js, which resides in the `client` folder.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

- Node.js (LTS version)
- npm

### Installation

To get started with this project, follow these steps:

1. Clone the repository:
   ```bash
   git clone https://github.com/fourstrides/prototype.service.git
   ```
2. Navigate to the client folder:

   ```bash
   cd prototype.service
   ```

3. Install the dependencies:

   ```bash
   npm install
   ```

4. Start the development server:
   ```bash
   npm run server
   ```

The application should now be running on http://localhost:9000.

## Client Folder

The `client` folder contains our Next.js application. We use **Shadcn UI** as our component library to build the UI efficiently and with a consistent style.



Set up environment variables:
Create a .env file in the root directory with the following variables:
port=9000

## Dependencies
Express.js
MongoDB (using mongoose)
dotenv (for environment variables)
Jest and Supertest (for testing)

## Testing
To run tests, use the following command:

npm test

## Deployment
For deployment to production, ensure to set up environment variables for the production environment.

## Contributing
Contributions are welcome! Please fork the repository and submit pull requests to contribute to the project.

## License
This project is licensed under the MIT License - see the LICENSE file for details.