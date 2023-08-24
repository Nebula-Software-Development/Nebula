![Total Downloads](https://img.shields.io/github/downloads/Nebuli-Team/Nebuli/total)


# Nebuli - A Powerful Plugin Framework for SCP:SL servers.

Nebuli is an extensible and feature-rich plugin framework designed for SCP:SL servers. It provides a flexible and easy-to-use platform for developers to create, manage, and distribute plugins to enhance the functionality and customization of game servers. Nebuli offers robust API interfaces, event handling, and seamless integration, making it a top choice for server owners and plugin developers alike.

## Key Features

- **Lightweight and Efficient:** Nebuli is designed to be lightweight and efficient, ensuring minimal performance impact on your SL server.

- **Plugin Management and Loading:** Easily manage and load plugins with Nebuli's intuitive plugin management system.

- **Event Handling and Customization:** Nebuli's powerful event system allows developers to customize and extend game server behavior with ease.

- **Easy Configuration System:** Configure and customize plugins using Nebuli's simple yet powerful configuration system.

## Getting Started

Follow these steps to get started with Nebuli:

1. **Installation:** Clone or download Nebuli from this GitHub repository and add it to your game server project.

2. **Configuration:** Configure Nebuli to your server's needs using the provided configuration files.

3. **Load Dependencies:** If your plugins have dependencies, place them in the designated directory and Nebuli will load them automatically.

4. **Creating Plugins:** Develop your own custom plugins by following the guidelines in the "Plugin Development" section below.

5. **Enable Nebuli:** Once your server is set up and Nebuli is integrated, start your server and Nebuli will handle the plugin loading process.

## Plugin Development

To create plugins for Nebuli, follow these steps:

1. **Create a New Project:** Start by creating a new project using the .NET framework, version 4.8.

2. **Add Nebuli Reference:** Add a reference to the Nebuli library in your project.

3. **Implement Plugin Interface:** In your plugin class, implement the `IPlugin<TConfig>` interface, where `TConfig` is your plugin's configuration class.

4. **Define Plugin Metadata:** Provide information about your plugin, such as name, author, version, etc., through the implemented interface properties.

5. **Event Handling:** Use Nebuli's event system to hook into game server events and customize behavior.

6. **Configuration:** Create a configuration class (implementing `IConfig`) to manage plugin settings and defaults.

7. **Compile Plugin:** Compile your plugin project into a .dll file.

8. **Load Your Plugin:** Place your compiled plugin .dll file into the designated plugin folder of your game server, and Nebuli will load it automatically.

## Contributing

We welcome contributions from the community! Whether you want to report a bug, suggest a feature, or submit a pull request, we appreciate your involvement in making Nebuli even better.

## License

Nebuli is licensed under the MIT License. See the `LICENSE` file for more details.

---

Visit the Nebuli discord and join our community!
