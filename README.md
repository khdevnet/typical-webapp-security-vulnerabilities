# Typical web app security vulnerabilities
## Injection
### Security Weakness
Injection flaws are very prevalent, particularly in legacy code. Injection vulnerabilities are often found in SQL, LDAP, XPath, or NoSQL queries, OS commands, XML parsers, SMTP headers, expression languages, and ORM queries.
Injection flaws are easy to discover when examining code. Scanners and fuzzers can help attackers find injection flaws.
### Impacts
Injection can result in data loss, corruption, or disclosure to unauthorized parties, loss of accountability, or denial of access. Injection can sometimes lead to complete host takeover.
### Example
Scenario #1: An application uses untrusted data in the construction of the following vulnerable SQL call:

```String query = "SELECT * FROM accounts WHERE custID='" + request.getParameter("id") + "'";```

Scenario #2: Similarly, an application’s blind trust in frameworks may result in queries that are still vulnerable, (e.g. Hibernate Query Language (HQL)):

```Query HQLQuery = session.createQuery("FROM accounts WHERE custID='" + request.getParameter("id") + "'");```

In both cases, the attacker modifies the ‘id’ parameter value in their browser to send: ' or '1'='1. For example:

http://example.com/app/accountView?id=' or '1'='1
This changes the meaning of both queries to return all the records from the accounts table. More dangerous attacks could modify or delete data, or even invoke stored procedures.
