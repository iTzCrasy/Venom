//=> Venom.API: 

--> Work in Progress:
	- Tables for the data
		--> GETDATE / GETUTCDATE (Check timezone)
	- Insert new data (OPENROWSET)

--> TODO: 
	- Add Caching (System.Runtime.Caching), Expire 1h 
		--> Only current data!
	- Add Update Cycle for the servers (Pull player, ally, ...)
	- Add Querys for select the data
		--> Check Timezone (!)

Table (Player):
- Full Entry / Hour
- int, datetime2, int, nvarchar, int, int
- World ID | Date | ID | Name | Points | Villages
[ ID = Compound Key - Index (World ID, Date, ID) ]

Table (Ally):
- Full Entry / Hour
- int, datetime2, int, nvarchar, nvarchar, int, int, int, int, int
- World ID | Date | ID | Name | Tag | Members | Villages | Points | AllPoints | Rank
[ ID = Compound Key - Index (World ID, Date, ID) ]

Table (Villages):
//=> Only Update? 
- int, datetime2, int, nvarchar, int, int, int, int, int
- World ID | Date | ID | Name | X | Y | Owner | Points | Bonus
[ ID = Compund Key - Index (World ID, Date, ID, X, Y) ]