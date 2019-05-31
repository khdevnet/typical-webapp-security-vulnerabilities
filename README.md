# Typical web app security vulnerabilities
## Injection
### Security Weakness
Injection flaws are very prevalent, particularly in legacy code. Injection vulnerabilities are often found in SQL, LDAP, XPath, or NoSQL queries, OS commands, XML parsers, SMTP headers, expression languages, and ORM queries.
Injection flaws are easy to discover when examining code. Scanners and fuzzers can help attackers find injection flaws.
### Impacts
Injection can result in data loss, corruption, or disclosure to unauthorized parties, loss of accountability, or denial of access. Injection can sometimes lead to complete host takeover.
### Example
Scenario #1: An application uses untrusted data in the construction of the following vulnerable SQL call:

```var sql = "SELECT * FROM product WHERE sku='{sku}' LIMIT 1";```

Scenario #2: Similarly, an application’s blind trust in frameworks may result in queries that are still vulnerable, 
(e.g. EF Core FromSql Method):

```
SecurityWeakness.Infrastructure.SQL.NotSecureProductSqlRepository:

public Product GetSingleBySku(string sku)
{
    var sql = $"SELECT * FROM product WHERE sku='{sku}' LIMIT 1";
    return context.Products.FromSql(sql).ToArray().Single();
}
```

In both cases, the attacker modifies the ‘sku’ parameter value in their browser to send:

``` 
http://localhost:62384/NotSecureProducts/Product?sku=p2';DELETE from product;SELECT '1
```

This changes the meaning of both queries to delete all the records from the product table. More dangerous attacks could modify or delete data, or even invoke stored procedures.

### Secure Example
Rewrite code using Linq Query, always use parameterization for raw SQL queries 
```
SecurityWeakness.Infrastructure.SQL.SecureProductSqlRepository:
public Product GetSingleBySku(string sku)
{
    return context.Products.FromSql($"SELECT * FROM {TableName} WHERE sku={{0}} LIMIT 1", sku).ToArray().Single();
}
```
## Resources
* [OWASP Top Ten 2017](https://www.owasp.org/index.php/Category:OWASP_Top_Ten_2017_Project)
