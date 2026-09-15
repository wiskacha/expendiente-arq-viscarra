# Parcial #2 - Arquitectura de Software 
## Caso: Biblioteca Municipal 

- Situación 1

Solución/Patrón elegido: Observer

En el plantemiento del problema se encuentra a un "módulo de préstamos" bastante sobrecargado en cuanto al área de notificaciones, además se proyecta para verse modificado una y posiblemente más veces en el futuro. Por esto el patrón "observer" parece un candidato ideal, permitiéndonos adjuntar o suscribir a los sectores interesados para que reciban la notificación sin perjudicar/modificar el módulo de prestamos. Por el contrario si no lo aplicaramos veríamos una constante manipulación sobre lo métodos de ese módulo, arriesgándonos a romperlo.

- Situación 2

Solución/Patrón elegido: Strategy

Tenemos bastantes tipos de socios en el sistema, cada uno de los cuales posee un tipo de cálculo diferente y en constante cambio. Con "strategy" podemos encapsular cada tipo de cálculo en una clase intercambiable, eliminando el condicional repetido dentro de los distintos módulos. Sin su aplicación cada modificación a estos calculos y añadido de nuevos, nos obligaría a editar los condicionales idénticos dentro de dos distintos módulos arriesgándonos a obtener resultados distintos al desincronizarlos. 

- Situación 3

Solución/Patrón elegido: