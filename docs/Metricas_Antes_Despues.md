# Reporte de Métricas de Calidad: Antes y Después

**Proyecto:** La Casa del Rincón Guatemalteco
**Herramienta de Análisis:** SonarCloud
**Periodo de Evaluación:** 05 de Noviembre 2026

---

## 1. Línea Base (Antes de la Intervención SQA)
Al inicio del proyecto, antes de implementar pruebas automatizadas y el pipeline de calidad, el código presentaba las siguientes condiciones:

*   **Pruebas Automatizadas:** 0 pruebas ejecutadas.
*   **Cobertura de Código:** 18.2%
*   **Bugs (Reliability):** 13 (detectados por el primer escaneo estático).
*   **Code Smells (Maintainability):** Múltiples advertencias por falta de validación de entidades y malas prácticas en el código.
*   **Pipeline CI/CD:** Inexistente. El código se subía directamente a la rama `main` sin validación.

## 2. Intervención Realizada
*   Se creó el proyecto `RinconGuatemalteco.Tests` con xUnit.
*   Se implementó el cálculo de cobertura con Coverlet.
*   Se configuró SonarCloud para el análisis estático.
*   Se implementó el pipeline en GitHub Actions con las etapas de Build, Test y Analyze.
*   Se protegió la rama `main` con Quality Gates.

## 3. Estado Final (Después de la Intervención SQA)
*(Nota: Estos datos se actualizan con las capturas finales del dashboard de SonarCloud)*

*   **Pruebas Automatizadas:** 1+ pruebas unitarias implementadas y ejecutadas en cada Push.
*   **Cobertura de Código:** 18.2%.
*   **Bugs (Reliability):** 0 (los 13 iniciales fueron corregidos o justificados).
*   **Code Smells:** Reducción significativa. El código crítico ahora cumple con las reglas de SonarWay.
*   **Pipeline CI/CD:** Activo. Cada Pull Request pasa por compilación, ejecución de pruebas y análisis estático.

## 4. Impacto y Conclusiones
La implementación del pipeline de calidad transformó el proceso de desarrollo:
1.  **Detección temprana de errores:** Las pruebas unitarias evitan que errores en la lógica de facturación lleguen a producción.
2.  **Estandarización:** SonarCloud obliga a mantener un código limpio (0 bugs críticos).
3.  **Automatización:** El equipo ya no depende de pruebas manuales para validar los cálculos matemáticos del sistema.

## 5. Evidencias
*   *<img width="1892" height="1037" alt="image" src="https://github.com/user-attachments/assets/f6c9dbf6-8da3-4b90-8994-b4ca71915a35" />
*   *<img width="1885" height="1051" alt="image" src="https://github.com/user-attachments/assets/f4027fcb-98e0-4b10-867d-6a14b604820d" />
*
