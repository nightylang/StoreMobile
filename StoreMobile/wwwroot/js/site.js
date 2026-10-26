
// =============================================
// Tab Visibility – Auto Pause/Resume Audio/Video
// =============================================
document.addEventListener('DOMContentLoaded', function () {
    let mediaPlayingState = [];

    function pauseAllMedia() {
        const mediaElements = document.querySelectorAll('audio, video');
        mediaPlayingState = [];
        mediaElements.forEach(media => {
            const wasPlaying = !media.paused;
            mediaPlayingState.push({ element: media, wasPlaying: wasPlaying });
            if (wasPlaying) {
                media.pause();
            }
        });
    }

    function resumeAllMedia() {
        mediaPlayingState.forEach(item => {
            if (item.wasPlaying) {
                item.element.play().catch(e => console.warn('Could not resume media:', e));
            }
        });
        mediaPlayingState = [];
    }

    document.addEventListener('visibilitychange', function () {
        if (document.hidden) {
            pauseAllMedia();
        } else {
            resumeAllMedia();
        }
    });
});

document.addEventListener('keydown', function (e) {
    const tag = e.target.tagName.toLowerCase();
    // Ignore if focused on input, textarea, or contenteditable
    if (tag === 'input' || tag === 'textarea' || e.target.isContentEditable) {
        return;
    }
    if ((e.ctrlKey || e.metaKey) && e.key === 'p') {
        e.preventDefault();
        window.location.href = '/Home/Product';
    }
});



window.addEventListener("load", function () {
    const loader = document.getElementById("loader-wrapper");
    const content = document.getElementById("main-content");

    loader.style.opacity = "0";

    setTimeout(function () {
        loader.style.display = "none";
        content.style.display = "block";
    }, 900);
});
const hamburger = document.getElementById('hamburger');
const navMenu = document.getElementById('navMenu');

hamburger.addEventListener('click', () => {
    navMenu.classList.toggle('active');

    // Optional: Animate hamburger lines into an 'X'
    hamburger.classList.toggle('open');
});

// Close menu when clicking a link (mobile UX optimization)
document.querySelectorAll('.nav-links a').forEach(link => {
    link.addEventListener('click', () => {
        navMenu.classList.remove('active');
        hamburger.classList.remove('open');
    });
});