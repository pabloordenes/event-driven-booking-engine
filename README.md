# Event-Driven Booking System

> Note: This repository is currently under active development.

An event-driven booking backend built in .NET 10, designed to handle high-concurrency scenarios and demonstrate cloud-native enterprise architecture principles. 

This project tackles common distributed system challenges, such as the Dual-Write problem and Double-Booking, by implementing robust design patterns and strict separation of concerns, ensuring readiness for deployment in Microsoft Azure environments.

## Core Architectural Patterns

*   **Clean Architecture**: The solution is divided into distinct layers (Domain, Application, Infrastructure, Presentation) enforcing the Dependency Inversion Principle. Core business logic remains entirely agnostic of external frameworks or UI.
*   **Optimistic Concurrency Control**: Implemented via Entity Framework Core (`RowVersion`). This ensures data integrity during high-load booking attempts without the severe performance bottlenecks of pessimistic database locks.
*   **Transactional Outbox Pattern**: Guarantees at-least-once message delivery and resolves the distributed transaction (Dual-Write) problem. Database writes and event creations are committed atomically, with a separate background worker relaying messages to the message broker.
*   **Ports and Adapters**: Infrastructure dependencies (like the event bus) are abstracted. The system utilizes `System.Threading.Channels` for local development and is structurally prepared to swap to Azure Service Bus and Azure Functions for production without altering business logic.

## Technology Stack

*   **.NET 10** (C#)
*   **Entity Framework Core** (SQL Server)
*   **Background Worker Services** (.NET Hosted Services)
*   **System.Threading.Channels** (In-Memory Event Bus simulation)
*   *Designed for:* Azure Service Bus, Azure Functions (Isolated Worker Model), and Azure SQL.

## Solution Structure

The project is structured into the following specific layers:

*   `BookingSystem.Domain`: Contains enterprise logic and core entities (`Booking`, `OutboxMessage`). No external dependencies.
*   `BookingSystem.Application`: Defines use cases, interfaces, and CQRS handlers. Orchestrates the flow of data.
*   `BookingSystem.Infrastructure`: Data access implementations, Entity Framework configurations, and external service adapters.
*   `BookingSystem.Api`: The primary HTTP entry point (REST/Minimal APIs).
*   `BookingSystem.Worker`: A background daemon responsible for polling the Outbox table and publishing integration events.
