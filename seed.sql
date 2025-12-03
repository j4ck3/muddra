-- Seed script for Services table
-- PostgreSQL

INSERT INTO "Services" ("Id", "Title", "Description", "ImageUrl") VALUES
  (gen_random_uuid()::text, 'Muddring', 'Muddrar med en gripskoppa.', 'muddring.webp'),
  (gen_random_uuid()::text, 'Pålning', 'Pålar med det material du önskar.', 'polning.webp'),
  (gen_random_uuid()::text, 'Bogsering', 'Bogsering och sjötransport.', 'bogsering.webp'),
  (gen_random_uuid()::text, 'Sonar', 'Sonarkörning för bottenkartering, sök efter vrak och grund.', 'sonar.webp'),
  (gen_random_uuid()::text, 'Sprängning', 'Spränger och spräcker sten ovan och under vatten.', 'sprangning.webp'),
  (gen_random_uuid()::text, 'Dykning', 'Dykbesiktning och bärgning.', 'dykning.webp'),
  (gen_random_uuid()::text, 'Ekolodsmätning', 'Mäter djupet runt t.ex. din brygga.', 'sonar.webp'),
  (gen_random_uuid()::text, 'Tillstånd', 'Behjälplig i ansökningsförfarande eller fullständig handläggning.', 'tillstond.webp'),
  (gen_random_uuid()::text, 'Sjötransport', 'Max 16 ton.', 'transport.webp'),
  (gen_random_uuid()::text, 'Sjötaxi', 'Sjötaxi på Hjälmaren. Max 8 pers.', 'taxi.webp'),
  (gen_random_uuid()::text, 'Annat', 'Beskriv ditt behov.', 'annat.webp')
ON CONFLICT DO NOTHING;
