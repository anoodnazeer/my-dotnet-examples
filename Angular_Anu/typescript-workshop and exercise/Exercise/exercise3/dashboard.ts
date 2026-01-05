import * as readline from "readline";

const rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout,
});

 
const ask = (q: string): Promise<string> => {
  return new Promise((resolve) => rl.question(q, resolve));
};

 

interface User {
  username: string;
  firstName: string;
  lastName: string;
  gender: string;
  phone: string;
}

let users: User[] = [];
let loggedInUser: User | null = null;

const jobs = [
  { id: 1, title: "Software Developer" },
  { id: 2, title: "System Administrator" },
  { id: 3, title: "UI/UX Designer" },
  { id: 4, title: "Data Analyst" },
];

let appliedJobs: { username: string; jobId: number }[] = [];

 

async function mainMenu() {
  console.log("\n===== MAIN MENU =====");
  console.log("1. Signup");
  console.log("2. Login");
  console.log("3. Exit");

  const choice = await ask("Choose an option: ");

  switch (choice) {
    case "1":
      signup();
      break;
    case "2":
      login();
      break;
    case "3":
      confirmExit();
      break;
    default:
      console.log("Invalid option. Try again.\n");
      mainMenu();
  }
}

 
async function signup() {
  console.log("\n===== SIGNUP =====");

  const username = await ask("Username: ");
  const firstName = await ask("First Name: ");
  const lastName = await ask("Last Name: ");
  const gender = await ask("Gender: ");
  const phone = await ask("Phone Number: ");

  const newUser: User = {
    username,
    firstName,
    lastName,
    gender,
    phone,
  };

  users.push(newUser);
  loggedInUser = newUser;

  console.log("\nSignup Successful! Logged in as:", username);
  userMenu();
}

 
async function login() {
  console.log("\n===== LOGIN =====");

  const username = await ask("Enter username: ");

  const user = users.find((u) => u.username === username);

  if (!user) {
    console.log("User not found. Try again.\n");
    mainMenu();
    return;
  }

  loggedInUser = user;
  console.log(`\nWelcome back, ${user.firstName}!`);
  userMenu();
}

 

async function userMenu() {
  console.log("\n===== USER MENU =====");
  console.log("1. View Listed Jobs");
  console.log("2. Apply for Job");
  console.log("3. View Applied Jobs");
  console.log("4. Logout");

  const choice = await ask("Choose an option: ");

  switch (choice) {
    case "1":
      viewJobs();
      break;
    case "2":
      applyForJob();
      break;
    case "3":
      viewAppliedJobs();
      break;
    case "4":
      logout();
      break;
    default:
      console.log("Invalid option! Try again.\n");
      userMenu();
  }
}

 

function viewJobs() {
  console.log("\n===== AVAILABLE JOBS =====");
  jobs.forEach((j) => console.log(`${j.id}. ${j.title}`));
  userMenu();
}

 

async function applyForJob() {
  console.log("\n===== APPLY FOR A JOB =====");

  jobs.forEach((j) => console.log(`${j.id}. ${j.title}`));

  const id = await ask("Enter job ID to apply: ");
  const jobId = Number(id);

  const job = jobs.find((j) => j.id === jobId);

  if (!job) {
    console.log("Invalid Job ID.\n");
    return userMenu();
  }

  const alreadyApplied = appliedJobs.some(
    (a) => a.username === loggedInUser!.username && a.jobId === jobId
  );

  if (alreadyApplied) {
    console.log("You already applied for this job.\n");
    return userMenu();
  }

  appliedJobs.push({ username: loggedInUser!.username, jobId });
  console.log(`Applied successfully for: ${job.title}\n`);
  userMenu();
}

 

function viewAppliedJobs() {
  console.log("\n===== APPLIED JOBS =====");

  const userApplied = appliedJobs.filter(
    (a) => a.username === loggedInUser!.username
  );

  if (userApplied.length === 0) {
    console.log("No applied jobs.\n");
    return userMenu();
  }

  userApplied.forEach((a) => {
    const job = jobs.find((j) => j.id === a.jobId);
    console.log(`${job!.id}. ${job!.title}`);
  });

  userMenu();
}

 

function logout() {
  console.log(`\nLogging out ${loggedInUser!.username}...\n`);
  loggedInUser = null;
  mainMenu();
}

 

async function confirmExit() {
  const c = await ask("Are you sure you want to exit? (yes/no): ");

  if (c.toLowerCase() === "yes") {
    console.log("Exiting program...");
    rl.close();
    process.exit(0);
  } else {
    mainMenu();
  }
}

 

mainMenu();
