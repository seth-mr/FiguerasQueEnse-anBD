# Datos de la Base de Datos - figurasqe

**Fecha de generacion:** 2026-05-09

---

## LEVELS (5 registros)

| id_level | Name | Difficulty |
|----------|------|------------|
| 1 | Beginner | 1 |
| 2 | Elementary | 2 |
| 3 | Intermediate | 5 |
| 4 | Advanced | 8 |
| 5 | Expert | 10 |

---

## TUTORS (5 registros)

| id_tutor | Name | Email | Password Hash | Country | Gender | Age | Grade | Registration Date |
|----------|------|-------|---------------|---------|--------|-----|-------|-------------------|
| 47 | Juan García | juan.garcia@mail.com | $2a$11$LJNg4QiK9v4ZaC/vrzVQz.GMdoz.phnzVlu3ky1SlWKDbdkzZfy/. | MX | M | 35 | Maestria | 2026-05-09 23:30:06 |
| 48 | María López | maria.lopez@mail.com | $2a$11$i2eX1GOzYfSOOFy9Bp.5l.6w9BViixG/CCwiK80SLJr4xopEfRCga | ES | F | 32 | Doctorado | 2026-05-09 23:30:06 |
| 49 | Carlos Rodríguez | carlos.rodriguez@mail.com | $2a$11$qOqcgPrUeY6LlLJ/TPNvouXb02gB7xPMagjoYqEFFRvQYzqWwqOY. | CO | M | 40 | Maestria | 2026-05-09 23:30:06 |
| 50 | Ana Martínez | ana.martinez@mail.com | $2a$11$RwnJs9JOu1hIH8e3Uo3mx.4q10A/ahTgvjTbht2rf..gcHpZxCcWa | AR | F | 28 | licenciatura | 2026-05-09 23:30:06 |
| 51 | Pedro Sánchez | pedro.sanchez@mail.com | $2a$11$aSGUkdASMCThqijut58T4uYOdW8fE6Yppr5/pl6DKBgkm69J7RznO | PE | M | 38 | Post Doctorado | 2026-05-09 23:30:06 |

---

## STUDENTS (10 registros)

| id_student | id_tutor | Name | Email | Password Hash | Age | Genre | Country | Neurodivergency | Registration Date |
|-----------|----------|------|-------|---------------|-----|-------|---------|-----------------|-------------------|
| 17 | 47 | Luis Martínez | luis.martinez@mail.com | $2a$11$Gx6yrG8ixQPigyB6LGu21Oih9zEngJVv2kH/OHXAok8JZd.8rhx5G | 15 | M | MX | ADHD | 2026-05-09 23:30:06 |
| 18 | 47 | Sofia García | sofia.garcia@mail.com | $2a$11$ggNJnKCiA6DIAJd/6Dmiy.7akVI8/gXlqHqWKpkICozG0T0TXEM86 | 16 | F | MX | Dyslexia | 2026-05-09 23:30:06 |
| 19 | 48 | Pablo Rodríguez | pablo.rodriguez@mail.com | $2a$11$5AyoFtXW77mIDuvsLS0OWOq96CvSZr1uNPYL1/qGsBKz2MpPIq.V6 | 14 | M | ES | NULL | 2026-05-09 23:30:06 |
| 20 | 48 | Carmen López | carmen.lopez@mail.com | $2a$11$ZRE4YVeLZXvyUHUBUNHgqebbOB19acVDMnfasvrkv4Qiz.c1PN.yu | 17 | F | ES | Autism Spectrum | 2026-05-09 23:30:06 |
| 21 | 49 | Diego Sánchez | diego.sanchez@mail.com | $2a$11$XyYGO6XWYi2mIn4UN9MVM.Ao/Yill9llqEzZS18t.UZ80q7gEVeKa | 15 | M | CO | ADHD | 2026-05-09 23:30:06 |
| 22 | 49 | Laura Gómez | laura.gomez@mail.com | $2a$11$kZRwiJg2XR69M75i9LsluOvyt21NymBZKBWtxNocUvEN3cXaku/Wy | 16 | F | CO | NULL | 2026-05-09 23:30:06 |
| 23 | 50 | Fernando Díaz | fernando.diaz@mail.com | $2a$11$/MRIVzIJ1f0bs/ktkTFHjO6/NVmiNGx3Tpt5Qz5yh.8Lw/88ioed. | 14 | M | AR | NULL | 2026-05-09 23:30:06 |
| 24 | 50 | Valentina Fernández | valentina.fernandez@mail.com | $2a$11$WSUhhhTBocbjqKuosO/IA.ooKICS7Ixb462Nu2JkJ54DGFWTaI7X6 | 18 | F | AR | Dyslexia | 2026-05-09 23:30:06 |
| 25 | 51 | Alejandro Torres | alejandro.torres@mail.com | $2a$11$iAhzi99WIDpeLEq6emRgU.0GzN49nncQMyfF5OD2sZL81s8iMG3Du | 15 | M | PE | NULL | 2026-05-09 23:30:06 |
| 26 | 51 | Martina Cruz | martina.cruz@mail.com | $2a$11$ci1uX6bVM4nDtteshfWnouTJh1kRkqo.Y6wSu.GgtYuLFJFrEApdO | 17 | F | PE | ADHD Dyslexia | 2026-05-09 23:30:06 |

---

## SESSIONS (10 registros)

| id_session | id_student | Beginning Date | End Date | Device |
|-----------|-----------|-----------------|----------|--------|
| 1 | 17 | 2026-05-01 10:00:00 | 2026-05-01 11:30:00 | Laptop |
| 2 | 18 | 2026-05-02 14:00:00 | 2026-05-02 15:45:00 | Tablet |
| 3 | 19 | 2026-05-03 09:00:00 | 2026-05-03 10:15:00 | Phone |
| 4 | 20 | 2026-05-04 16:00:00 | 2026-05-04 17:30:00 | Laptop |
| 5 | 21 | 2026-05-05 11:00:00 | 2026-05-05 12:45:00 | Desktop |
| 6 | 22 | 2026-05-06 13:00:00 | 2026-05-06 14:30:00 | Tablet |
| 7 | 23 | 2026-05-07 10:30:00 | 2026-05-07 11:45:00 | Phone |
| 8 | 24 | 2026-05-08 15:00:00 | 2026-05-08 16:45:00 | Laptop |
| 9 | 25 | 2026-05-09 09:30:00 | 2026-05-09 10:45:00 | Desktop |
| 10 | 26 | 2026-05-09 14:00:00 | 2026-05-09 15:30:00 | Tablet |

---

## LEVEL RESULTS (20 registros)

| id_result | id_session | id_level | Finishing Time (seconds) | Attempts | Fails | Completed |
|----------|-----------|----------|-------------------------|----------|-------|-----------|
| 1 | 1 | 1 | 1800 | 1 | 0 | True |
| 2 | 1 | 2 | 2400 | 2 | 1 | True |
| 3 | 2 | 1 | 1500 | 1 | 0 | True |
| 4 | 2 | 3 | 3600 | 3 | 2 | False |
| 5 | 3 | 2 | 2100 | 2 | 1 | True |
| 6 | 3 | 4 | 4200 | 4 | 3 | True |
| 7 | 4 | 1 | 1200 | 1 | 0 | True |
| 8 | 4 | 3 | 3300 | 3 | 1 | True |
| 9 | 5 | 2 | 2700 | 2 | 1 | True |
| 10 | 5 | 5 | 5400 | 5 | 4 | False |
| 11 | 6 | 3 | 3000 | 3 | 2 | True |
| 12 | 6 | 4 | 4500 | 4 | 2 | False |
| 13 | 7 | 1 | 1600 | 1 | 0 | True |
| 14 | 7 | 2 | 2300 | 2 | 1 | True |
| 15 | 8 | 4 | 4000 | 4 | 3 | True |
| 16 | 8 | 5 | 5100 | 5 | 3 | True |
| 17 | 9 | 2 | 2200 | 2 | 1 | True |
| 18 | 9 | 3 | 3500 | 3 | 2 | True |
| 19 | 10 | 3 | 3400 | 3 | 2 | True |
| 20 | 10 | 5 | 5500 | 5 | 4 | False |

---

## RESUMEN

| Tabla | Total Registros |
|-------|-----------------| 
| Levels | 5 |
| Tutors | 5 |
| Students | 10 |
| Sessions | 10 |
| Level Results | 20 |
| **TOTAL** | **50** |

---
