# Plan de Aseguramiento de la Calidad (SQA Plan)
**Proyecto:** La Casa del Rincón Guatemalteco - Sistema de Facturación y Caja
**Estudiante:** Gerson Daniel Xón Sic
**Curso:** Aseguramiento de la Calidad del Software (SQA)
**Fecha:** 05 de Noviembre 2026

---

## 1. Introducción
Este documento describe el Plan de Aseguramiento de la Calidad (SQA) aplicado al sistema web "La Casa del Rincón Guatemalteco". El objetivo es garantizar que el software cumpla con los estándares de calidad, funcionalidad y seguridad mediante la implementación de pruebas automatizadas, análisis estático y un pipeline de Integración Continua (CI).

## 2. Alcance
El plan cubre el ciclo de vida de desarrollo del backend (ASP.NET Core MVC) y la base de datos relacional. Se enfoca en la validación de la lógica de negocio crítica (cálculo de totales, gestión de inventario, cierre de caja) y en la prevención de regresiones mediante pruebas unitarias automatizadas.

## 3. Herramientas de Calidad Utilizadas
*   **Framework de Pruebas:** xUnit (para pruebas unitarias en C#).
*   **Cobertura de Código:** Coverlet (para medir el porcentaje de código evaluado por las pruebas).
*   **Análisis Estático:** SonarCloud (para detectar bugs, vulnerabilidades y code smells).
*   **CI/CD:** GitHub Actions (para automatizar la construcción, pruebas y análisis en cada cambio).
*   **Control de Versiones:** Git y GitHub.

## 4. Estrategia de Pruebas
Para este proyecto, se han definido los siguientes niveles de prueba:
*   **Pruebas Unitarias:** Validan la lógica de negocio aislada (ej. el cálculo correcto de los puntos de fidelidad o el total de una factura). Se ejecutan automáticamente en el pipeline.
*   **Pruebas de Integración:** Verifican la interacción entre el controlador y la base de datos (usando Entity Framework Core InMemory).
*   **Pruebas de Regresión:** Se ejecuta toda la suite de pruebas en cada "Push" para asegurar que los nuevos cambios no rompan funcionalidades existentes.

## 5. Quality Gates (Umbrales de Calidad)
El pipeline fallará y bloqueará el "Merge" de cualquier Pull Request si no se cumplen los siguientes umbrales mínimos:
*   **Cobertura de código (Coverage):** ≥ 60% (según los requisitos del curso).
*   **Bugs Críticos:** 0.
*   **Vulnerabilidades de Seguridad:** 0.
*   **Code Smells:** Menos de 5 en el nuevo código.

## 6. Matriz de Pruebas (Módulos Críticos)
| Módulo | Caso de Prueba | Resultado Esperado |
| :--- | :--- | :--- |
| Facturación | Procesar una venta con 2 productos iguales | El subtotal se suma correctamente y el stock baja en 2. |
| Facturación | Aplicar descuento de puntos | Si el cliente tiene 100 puntos, se descuentan Q10 del total. |
| Caja | Cerrar caja con ventas en efectivo y tarjeta | El total en caja solo refleja el efectivo físico. |
| Caja | Intentar cerrar caja sin haberla abierto | El sistema debe bloquear la acción o mostrar error. |

## 7. Gestión de Deuda Técnica
Cualquier "Code Smell" detectado por SonarCloud que no pueda ser corregido en la fase actual, será documentado en el backlog del proyecto y priorizado para la siguiente iteración.

## 8. Roles y Responsabilidades
*   **Desarrollador / QA:** Daniel Xón. Responsable de escribir las pruebas, configurar el pipeline y corregir los issues detectados por SonarCloud.

## 9. Entregables
*   Código fuente con pruebas unitarias.
*   Pipeline de CI/CD funcional (`ci.yml`).
*   Reporte de métricas antes y después.
*   Documentación de la API y manual de usuario.
