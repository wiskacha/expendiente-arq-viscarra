# Parcial #2 - Arquitectura de Software 
## Caso: Biblioteca Municipal 

- Situación 1

Solución/Patrón elegido: Observer

En el plantemiento del problema se encuentra a un "módulo de préstamos" bastante sobrecargado en cuanto al área de notificaciones, además se proyecta para verse modificado una y posiblemente más veces en el futuro. Por esto el patrón "observer" parece un candidato ideal, permitiéndonos adjuntar o suscribir a los sectores interesados para que reciban la notificación sin perjudicar/modificar el módulo de prestamos. Por el contrario si no lo aplicaramos veríamos una constante manipulación sobre lo métodos de ese módulo, arriesgándonos a romperlo.

- Situación 2

Solución/Patrón elegido: Strategy

Tenemos bastantes tipos de socios en el sistema, cada uno de los cuales posee un tipo de cálculo diferente y en constante cambio. Con "strategy" podemos encapsular cada tipo de cálculo en una clase intercambiable, eliminando el condicional repetido dentro de los distintos módulos. Sin su aplicación cada modificación a estos calculos y añadido de nuevos, nos obligaría a editar los condicionales idénticos dentro de dos distintos módulos arriesgándonos a obtener resultados distintos al desincronizarlos. 

- Situación 3

Solución/Patrón elegido: Adapter

Los datos qué percibimos desde el servicio externo es una constante; no la podemos modificar, posee un formato, códigos y métodos ajenos a nuestro dominio. Con "adapter" podemos introducir un "puesto fronterizo" qué se encarge de traducir el orden, formato y códigos necesarios a unos qué nuestro sistema/dominio no sólo maneje, sino que espere recibir de ellos, sanando la conexión qué, de otra forma, implicaría realizar arreglos manuales para cada caso de información recibida(variables, funciones, calculos y fechas) dependiendo de la fuente/origen y esto se repetiría cada vez qué el servicio externo actualizara sus formatos.

## Post-implementación en código : Situación #1 

Principio SOLID: open-close

El principio de open-close se ve rescatado en la propia definición del Módulo de Préstamos (líneas 39-51) al este hallarse cerrado, incluso posteriormente podríamos ver agregados nuevos interesados en recibir una notificación respecto a los atrasos y sólo tendríamos que asegurarnos de que la nueva clase implementara nuestra interfaz IObservador y suscribirlos al módulo mediante el método "Suscribir" durante la ejecución sin modificar en absoluto el código que yace dentro del propio módulo. 