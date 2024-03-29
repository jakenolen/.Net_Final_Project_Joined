use cs340318

/* ------------------ SELECT ------------------ */
SELECT *
FROM ARCEMP;

SELECT * 
FROM MAINFRAME;

SELECT *
FROM WEB;

SELECT * 
FROM DESKTOP;

/* ------------------INSERT ------------------ */

INSERT INTO ARCEMP VALUES('UIDTEST4', 'PASSTEST4', 'FNAMETEST4', 'LNAMETEST4', 'ADT');

INSERT INTO MAINFRAME VALUES();

INSERT INTO WEB VALUES();

INSERT INTO DESKTOP VALUES();


/* ------------------ DROP ------------------ */

DROP TABLE ARCEMP;
USE CS340318
DROP TABLE MAINFRAME;
DROP TABLE WEB;
DROP TABLE DESKTOP;

/* ------------------ CREATE ------------------ */
CREATE TABLE ARCEMP(
	ID				INT NOT NULL IDENTITY(1000,1),
	USERNAME		VARCHAR(20),
	PASS			VARCHAR(20),
	FNAME			VARCHAR(30),
	LNAME			VARCHAR(45),
	ROLE			VARCHAR(3),
	PRIMARY KEY(ID)
);

CREATE TABLE MAINFRAME(
	ID				INT NOT NULL IDENTITY(10000,1),
	DEVID			INT,
	MGNID			INT,
	REVIEWED		VARCHAR(3),
	ENV_TYPE		VARCHAR(10),
	MEMBER			VARCHAR(50),
	PUSH_DATE		VARCHAR(50),
	PROJECT_NAME	VARCHAR(50),
	ACTION_DESC		VARCHAR(50),
	INSTALLER		VARCHAR(50),
	INSTALLFORMID	VARCHAR(50),
	INSTALL_DESC	VARCHAR(255),
	COMMENTS		VARCHAR(255),
	PRIMARY KEY(ID),
	FOREIGN KEY(DEVID) REFERENCES ARCEMP(ID),
	FOREIGN KEY(MGNID) REFERENCES ARCEMP(ID)
);

CREATE TABLE WEB(
	ID				INT NOT NULL IDENTITY(10000,1),
	DEVID			INT,
	MGNID			INT,
	REVIEWED		VARCHAR(3),
	ENV_TYPE		VARCHAR(10),
	PROJECT_NAME	VARCHAR(50),
	MEMEMBER		VARCHAR(50),
	FROMDIR			VARCHAR(50),
	INSTALLID		VARCHAR(50),
	DESTSRV			VARCHAR(50),
	DESTDIR			VARCHAR(50),
	PUBLISHED		VARCHAR(50),
	PUSH_DATE		VARCHAR(50),
	INSTALL_DESC	VARCHAR(50),
	AGENDA_NUM		VARCHAR(50),
	REVIEWED_BY		VARCHAR(50),
	REQUESTED_BY	VARCHAR(50),
	INSTALL_RES		VARCHAR(50),
	LAST_UPDATE		VARCHAR(50),
	THIRD_PARTY		VARCHAR(50),
	ERROR			VARCHAR(50),
	ERROR_DESC		VARCHAR(255),
	COMMENTS		VARCHAR(255),
	PRIMARY KEY(ID),
	FOREIGN KEY(DEVID) REFERENCES ARCEMP(ID),
	FOREIGN KEY(MGNID) REFERENCES ARCEMP(ID)
);

CREATE TABLE DESKTOP(
	ID				INT NOT NULL IDENTITY(10000,1),
	DEVID			INT,
	MGNID			INT,
	REVIEWED		VARCHAR(3),
	ENV_TYPE		VARCHAR(10),
	PROJECT_NAME	VARCHAR(50),
	DEP_TO			VARCHAR(50),
	DEP_BY			VARCHAR(50),
	INSTALL_FORM	VARCHAR(50),
	BUILD_SORCE		VARCHAR(50),
	BUILD_DETAILS	VARCHAR(50),
	PAC_DETAILS		VARCHAR(50),
	RELEASE_DETAILS	VARCHAR(50),
	PROJECT_DETAILS VARCHAR(50),
	COMMENTS		VARCHAR(255),
	PRIMARY KEY(ID),
	FOREIGN KEY(DEVID) REFERENCES ARCEMP(ID),
	FOREIGN KEY(MGNID) REFERENCES ARCEMP(ID)
);