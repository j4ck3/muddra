#!/bin/bash
set -e

echo "Restoring NuGet packages..."
dotnet restore || true

echo "Installing npm dependencies..."
cd /app && npm install || true

echo "Building Tailwind CSS..."
cd /app && npm run tailwind:build || true

echo "Starting Tailwind CSS watch in background..."
cd /app && npm run tailwind:watch > /tmp/tailwind.log 2>&1 &
TAILWIND_PID=$!

# Function to cleanup on exit
cleanup() {
    echo "Stopping Tailwind watch..."
    kill $TAILWIND_PID 2>/dev/null || true
    exit 0
}

trap cleanup SIGTERM SIGINT

echo "Tailwind watch started with PID: $TAILWIND_PID"
echo "Starting dotnet watch with hot reload..."

exec dotnet watch run --urls http://0.0.0.0:8080

