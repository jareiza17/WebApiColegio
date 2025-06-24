document.getElementById("enrollmentForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const studentId = document.getElementById("studentSelect").value;
    const selectedCheckboxes = document.querySelectorAll(".subject-checkbox:checked");

    if (!studentId) {
        alert("Por favor selecciona un estudiante.");
        return;
    }

    if (selectedCheckboxes.length === 0) {
        alert("Por favor selecciona al menos una materia.");
        return;
    }

    const selectedSubjects = [];
    let highCreditCount = 0;

    selectedCheckboxes.forEach(cb => {
        const subjectId = parseInt(cb.value);
        const credits = parseInt(cb.getAttribute("data-credits"));

        if (credits > 4) highCreditCount++;

        selectedSubjects.push(subjectId);
    });

    if (highCreditCount > 3) {
        alert("No puedes inscribir más de 3 materias con más de 4 créditos.");
        return;
    }

    const data = {
        studentID: parseInt(studentId),
        subjectIDs: selectedSubjects
    };

    try {
        const response = await fetch("https://localhost:7148/api/school/enrollments", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        if (!response.ok) {
            const error = await response.text();
            throw new Error(error);
        }

        alert("Materias inscritas exitosamente.");
        location.reload();
    } catch (err) {
        alert("Error al inscribir materias: " + err.message);
    }
});
