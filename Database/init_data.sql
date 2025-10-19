-- Rooms
INSERT INTO "Rooms" ("Id", "Code", "BedCount") VALUES
('62088082-a8c4-4d5d-acea-025099ad3bea', '1001', 2),
('ecaa3c41-5db3-44c3-b04e-89fb8744c8b0', '1002', 3);

-- TravelGroups
INSERT INTO "TravelGroups" ("Id", "GroupId", "ArrivalDate") VALUES
('04918f43-3b23-4d03-9fe4-e64863e0cc3e', 'G0001A', '2025-10-19'),
('1d8dd1f2-bc7d-4030-bd43-eb1256326901', 'G0001B', '2025-10-19');

-- Travellers
INSERT INTO "Travellers" 
("Id", "FirstName", "LastName", "DateOfBirth", "TravelGroupId", "RoomId") 
VALUES
('b8b303b6-cd7b-440c-8534-3e24b73e16ea', 'John', 'Doe', '1990-01-01', 'G0001A', '62088082-a8c4-4d5d-acea-025099ad3bea'),
('6c431cb5-159f-4fb0-8644-09c9f9d4d30f', 'Jane', 'Smith', '1992-02-02', 'G0001A', '62088082-a8c4-4d5d-acea-025099ad3bea'),
('ca93f9b3-e7bb-474e-9a14-f4e0548b5f2b', 'Alice', 'Brown', '1988-03-03', 'G0001B', 'ecaa3c41-5db3-44c3-b04e-89fb8744c8b0');
INSERT INTO public."Travellers"
("Id", "FirstName", "LastName", "DateOfBirth", "TravelGroupId", "RoomId")
VALUES
('92ae4a8f-f0fa-4ad8-b26e-65680d349d50', 'Ben', 'Klock', '1988-03-03', 'G0001B', NULL),
('28b10f1b-41af-475d-90c0-ddf49cc96cd8', 'Alice', 'Cooper', '1988-03-03', 'G0001B', NULL),
('1929a4df-b9bd-4e70-839a-7abbfc7ab1e7', 'Mary', 'Loren', '1988-03-03', 'G0001B', NULL);


