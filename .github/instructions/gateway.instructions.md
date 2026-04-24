---
description: "Use when editing Express routes, proxy logic, middleware, or environment config in FigurasQE-Gateway."
name: "API Gateway Instructions"
applyTo: "FigurasQE-Gateway/**"
---
# Gateway Instructions

## Scope
- Applies only to `FigurasQE-Gateway/` (Node.js, Express 5, `axios` for proxying).
- Gateway is the single entry point for the frontend — all client traffic goes through here.

## Routing Conventions
- Auth routes (`/auth/login`, `/auth/register`) in `routes/auth.js` proxy to `AUTH_SERVICE` env var.
- Data routes (`/data/students`) in `routes/students.js` proxy to `DATA_SERVICE` env var.
- New routes go in `src/routes/` as separate files, mounted in `src/server.js`.

## Environment Config
- All upstream URLs are read from environment variables via `dotenv`. Never hardcode service URLs.
- Required `.env` keys: `AUTH_SERVICE`, `DATA_SERVICE`, optionally `PORT` (default `3000`).
- A `.env` file is required but not committed — create one locally before running.

## Auth Header Forwarding
- Current routes do **not** forward the `Authorization` header to upstream services — this is a known gap.
- When implementing protected data routes, extract the `Authorization` header from the incoming request and pass it in the `axios` request headers.

## Known WIP
- No `start` script in `package.json` — run with `node src/server.js`.
- `students.js` does not forward the `Authorization` header; add `headers: { Authorization: req.headers.authorization }` to the axios call for protected routes.

## Quick References
- `FigurasQE-Gateway/src/server.js`
- `FigurasQE-Gateway/src/routes/`
- `FigurasQE-Gateway/package.json`
