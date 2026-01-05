import * as readline from "readline";

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

const jobProviders = [
  "Amplitude Software solution",
  "Aitrich",
  "Econcept",
  "TechnoPark"
];

const registrations = [
  { name: "Afreeen", status: "pending" },
  { name: "Nasif", status: "on-hold" },
  { name: "Kannan", status: "candidate" },
  { name: "Meera", status: "pending" },
  { name: "Anu", status: "candidate" },
];

const ask = (q: string): Promise<string> => {
  return new Promise((resolve) => rl.question(q, (ans) => resolve(ans)));
};

async function login() {
  console.log("\n===== Login =====");
  const email = await ask("Email: ");
  const password = await ask("Password: ");

  // Simple dummy login
  if (email === "admin@gmail.com" && password === "admin123") {
    console.log("\nLogin Successful!\n");
    dashboard();
  } else {
    console.log("\nInvalid credentials. Try again.\n");
    login();
  }
}

async function dashboard() {
  console.log("===== DASHBOARD =====");
  console.log("1. Job Providers List");
  console.log("2. New Registrations");
  console.log("3. Registrations - Pending");
  console.log("4. Registrations - On-Hold");
  console.log("5. Registrations - Candidate");
  console.log("6. Back to Dashboard (Reload)");
  console.log("7. Exit");

  const choice = await ask("Choose an option: ");

  switch (choice) {
    case "1":
      showJobProviders();
      break;

    case "2":
      showRegistrations("all");
      break;

    case "3":
      showRegistrations("pending");
      break;

    case "4":
      showRegistrations("on-hold");
      break;

    case "5":
      showRegistrations("candidate");
      break;

    case "6":
      console.log("\nReturning to Dashboard...\n");
      dashboard();
      break;

    case "7":
      confirmExit();
      break;

    default:
      console.log("Invalid option! Try again.\n");
      dashboard();
  }
}

function showJobProviders() {
  console.log("\n=== Job Providers List ===");
  jobProviders.forEach((p, i) => console.log(`${i + 1}. ${p}`));
  console.log("");
  dashboard();
}

function showRegistrations(type: string) {
  console.log(`\n=== Registrations (${type}) ===`);

  let list =
    type === "all"
      ? registrations
      : registrations.filter((r) => r.status === type);

  if (list.length === 0) {
    console.log("No records found.\n");
  } else {
    list.forEach((r, i) => console.log(`${i + 1}. ${r.name} - ${r.status}`));
  }

  console.log("");
  dashboard();
}


async function confirmExit() {
  const c = await ask("Are you sure you want to exit? (yes/no): ");

  if (c.toLowerCase() === "yes") {
    console.log("Exiting program...");
    rl.close();
    process.exit(0);
  } else {
    console.log("\nReturning to Dashboard...\n");
    dashboard();
  }
}

login();