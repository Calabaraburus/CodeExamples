![Class Diagram](http://www.plantuml.com/plantuml/proxy?src=https://raw.githubusercontent.com/Calabaraburus/CodeExamples/master/SQL/ProductsCategories/products.puml)

## SQL EXAMPLE

```SQL
SELECT p.name as productName, c.name as categoryName FROM productToCategory as ptc
right join product as p on p.id= ptc.idProduct
left join category as c on c.id=ptc.idCategory
```
