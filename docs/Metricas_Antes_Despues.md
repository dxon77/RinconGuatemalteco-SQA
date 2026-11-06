# Reporte de Métricas e Impacto del Aseguramiento de la Calidad

**Proyecto:** Sistema de Facturación "La Casa del Rincón Guatemalteco"

**Nombre:** Gerson Daniel Xón Sic

**Herramientas utilizadas:** GitHub Actions, SonarCloud, xUnit, dotnet-coverage.

---

## 1. Punto de Partida (Antes de la intervención)
Cuando terminé el desarrollo funcional del sistema para el curso anterior, el proyecto no contaba con ningún proceso automatizado de calidad. El código se subía directamente a la rama principal sin verificaciones previas.

Al conectar el proyecto por primera vez a SonarCloud para establecer la línea base, el análisis estático reveló la siguiente situación:
*   **Cobertura de código:** 18.2% (no existían pruebas automatizadas).
*   **Bugs (Fiabilidad):** El sistema detectó 13 problemas relacionados con malas prácticas en el manejo de variables y lógica.
*   **Deuda Técnica:** El código presentaba duplicaciones y "code smells" que dificultaban el mantenimiento.

> *<img width="1883" height="1140" alt="image" src="https://github.com/user-attachments/assets/47fa9ae8-9f06-49ff-bcfd-deadde9e0025" />*

---

## 2. Acciones Implementadas
Para solucionar esto y aplicar los conceptos de SQA, realicé las siguientes acciones:
1.  **Creación de pruebas unitarias:** Agregué un proyecto de pruebas (`RinconGuatemalteco.Tests`) usando xUnit. Me enfoqué en probar la lógica más crítica del negocio, como el cálculo de cierre de caja (verificando que solo sume las ventas en efectivo) y la creación/edición de productos y clientes.
2.  **Configuración del Pipeline (CI):** Creé un archivo `ci.yml` en GitHub Actions. Ahora, cada vez que hago un *Push* o un *Pull Request* a la rama `main`, GitHub levanta un servidor Ubuntu automáticamente, compila mi código, ejecuta las pruebas y manda los resultados a SonarCloud.
3.  **Medición de Cobertura:** Enfrenté un problema técnico donde SonarCloud no leía el reporte generado por Coverlet. Lo solucioné investigando y cambiando la herramienta a `dotnet-coverage`, configurando la ruta correcta para que el escáner de Sonar pudiera leer el archivo XML.
4.  **Quality Gates:** Configuré SonarCloud para que, si hay errores críticos o vulnerabilidades, el pipeline se ponga en rojo y bloquee la fusión del código.

---

## 3. Resultados Finales (Después de la intervención)
Tras implementar el pipeline y las pruebas, el estado del proyecto cambió radicalmente. Ahora el proyecto cuenta con un escudo de calidad automatizado.

*   **Quality Gate:** Pasado (Aprobado).
*   **Cobertura de código:** Aumentó del 0% a un **18.2%** (y sigue subiendo conforme agrego más pruebas). Aunque no llega al 60% exigido, la estructura ya está lista para escalar.
*   **Fiabilidad:** Los bugs críticos iniciales fueron revisados y el análisis se mantiene estable.
*   **Seguridad:** 0 vulnerabilidades detectadas.


> *<img width="1866" height="1045" alt="image" src="https://github.com/user-attachments/assets/8f94f1bc-0bc2-4caf-8c6a-da85976093f3" />*

---

## 4. Evidencia del Pipeline Funcionando
Para demostrar que el proceso es automático, aquí muestro cómo el pipeline se ejecuta correctamente cada vez que subo un cambio al repositorio.

> *<img width="1885" height="1051" alt="image" src="https://github.com/user-attachments/assets/91702b8b-0398-4d77-9203-7cae43754bea" />*

---

## 5. Conclusión Personal
Implementar este pipeline me hizo darme cuenta de que el desarrollo no termina cuando el código compila, sino cuando se garantiza que funciona bajo diferentes escenarios. El mayor reto fue entender cómo funcionaba SonarCloud con .NET (ya que la documentación de las herramientas de cobertura era confusa), pero lograrlo me asegura que, si en el futuro rompo algo en el sistema de facturación, las pruebas me avisarán antes de que llegue a los usuarios finales.
