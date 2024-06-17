# prototype.service
# Fourstrides Prototype Service Repository

Welcome to the Fourstrides company repository. This repository is home to our prototype service built with node js, which resides in the `service` folder.

### Prerequisites

Ensure you have Node.js and npm installed on your machine.

- Node.js (v20.13.1)
- npm (10.5.2)

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
4. Navigate to the client folder:

   ```bash
   cd src
   ```
5. Start the development server:
   ```bash
   npm run server
   ```


The application should now be running on http://localhost:9000.


Set up environment variables:

Create a .env file in the root directory with the following variables:

port=9000

## Dependencies

Express.js

MongoDB (using mongoose)

dotenv (for environment variables)

Jest and Supertest (for testing)

## Project Structure

Here's an overview of the folder structure within `service`:

- `src/`: This is the main source directory where the core application code resides.

- `controller/`: Receive incoming HTTP requests, extract relevant data (e.g., parameters, request body), and invoke the corresponding use case from the domain layer. They are responsible for handling various HTTP status codes and formatting responses.

- `domain/`: The domain layer encapsulates the core business logic and entities of your application. It defines what your application does and models the problem domain it addresses.

- `routes/`: Define how different HTTP endpoints map to specific controllers or handlers. This typically involves using a routing framework like Express.js in Node.js applications.

- `server/`: Adapters bridge the gap between the application-specific use cases and the external frameworks, libraries, or services. This folder may contain implementations of repositories, controllers, presenters, or other adapters that interface with external systems such as databases, web frameworks (e.g., Express.js), external APIs, or any other infrastructure-related concerns.

- `util/`: This folder contains the external frameworks and tools used by the application. It may include configuration files, utility functions, middleware, or any other code that directly interacts with or configures external dependencies like databases, web servers, logging frameworks, etc.

- `server.js`: This file typically serves as the entry point of your application. It may bootstrap the application, configure dependency injection (if used), and start the server or application logic.

- `tests/`: This directory contains test cases for your application. It's often organized into subdirectories such as unit/ for unit tests and integration/ for integration tests.

Other Files
- `.gitignore`: Specifies files and directories that should be ignored by version control (e.g., node_modules, build artifacts).
- `package.json`: Manifest file for Node.js projects, specifying dependencies and scripts.
- `README.md`: Documentation file providing an overview of the project, its structure, and instructions for usage, installation, and more.

## Testing
To run tests, use the following command:

npm test

## Deployment
For deployment to production, ensure to set up environment variables for the production environment.

## Contributing
Contributions are welcome! Please fork the repository and submit pull requests to contribute to the project.

## License
This project is licensed under the MIT License - see the LICENSE file for details.