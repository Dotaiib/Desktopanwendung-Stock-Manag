create database STManag
use STManag
Go

create table Enter(
Name varchar(max) not null,
PWD varchar(max) not null,
);

create table DB_ST_M(
ID Int PRIMARY KEY NOT NULL identity,
PName nvarchar(max) NOT NULL,
Lot nvarchar(max) NOT NULL,
N_Palette nvarchar(max) NOT NULL,
Location nvarchar(max) NOT NULL,
Date_Time DateTime default getdate(),
Date_Time_Update DateTime Null
);

create table DB_ST_M_SRT(
ID_SRT Int PRIMARY KEY NOT NULL identity,
Id_Old int NOT NUll,
PName nvarchar(max) NOT NULL,
Lot nvarchar(max) NOT NULL,
N_Palette nvarchar(max) NOT NULL,
Location nvarchar(max) NOT NULL,
Date_Time DateTime default getdate(),
Up_Date_Time_Update DateTime Null,
Srt_Date DateTime NOT NULL,
Symbl varchar(20) NOT NULL
);

insert into Enter values ('Cibel','Cibel')
insert into DB_ST_M values ('08ET12145654','0020B','0600','B3',getdate())

select * from  DB_ST_M_SRT 

select * from  DB_ST_M order by Date_Time desc



/*Multiple Search on the same box */
select * from  DB_ST_M where Lot like '123%' or Location like '123%' or N_Palette like '123%' or PName like'123%'

/*insert to the other DB*/
insert into DB_ST_M_SRT select Id, PName, Lot, N_Palette, Location, Date_Time, Date_Time_Update, '2020-07-21', 'Out' from DB_ST_M where N_Palette='00001'
insert into DB_ST_M_SRT select Id, PName, Lot, N_Palette, Location, Date_Time, Date_Time_Update, getdate(), Symbl='Out' from DB_ST_M where Location = '" + comboBox1.Text+"' and N_Palette = '"+textBox1.Text+"'

/*to delete from the first database*/
delete from DB_ST_M where ID IN (select ID from DB_ST_M, DB_ST_M_SRT where DB_ST_M_SRT.Id_Old=DB_ST_M.ID)

/*Trigger*/
create trigger refresh_database on DB_ST_M_SRT
after insert
as delete from DB_ST_M where ID IN (select ID from DB_ST_M, DB_ST_M_SRT where DB_ST_M_SRT.Id_Old=DB_ST_M.ID)

/*Search before make the product out*/
select * from DB_ST_M where Location like 'a2' and N_Palette like '00001'

/*Update data*/
update DB_ST_M set Location='B1', Date_Time_Update=getdate() where  ID='20'

/*Show all updated data*/
select * from DB_ST_M where Date_Time_Update>'1900' order by Date_Time_Update desc




