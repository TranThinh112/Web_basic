import { apiFetch } from "./Api_Fetch.js";
import {
    avatarUrlOf,
    conversationName,
    createAvatar,
    createMessageAvatar,
    directChatUserId,
    groupMemberIds,
    hasPendingRequest,
    isFriend,
    itemId,
    messageConversationId,
    messageSender,
    readMessagePage,
    renderAvatarInto,
    setEmpty,
    sortByLatestConversation,
    sortMessagesByTime,
    userDisplayName,
    userField,
    userName,
    valueOr
} from "./ChatHelper.js";

// Hàm lấy nhanh element theo id để code bên dưới ngắn gọn hơn.
const el = id => document.getElementById(id); 
// <=>function el(id) {
    // return document.getElementById(id); }

// Sidebar chứa danh sách nhóm chat, bạn bè, tìm kiếm và thông tin user hiện tại.
const sidebar = el("chatSidebar"); //const sidebar = document.getElementById("chatSidebar");

// Nút đóng sidebar trên màn hình nhỏ.
const closeSidebarButton = el("closeSidebarBtn");

// Khu vực render danh sách nhóm chat.
const groupList = el("groupList");

// Khu vực render danh sách bạn bè/direct chat.
const friendList = el("friendList");

// Khu vực chính hiển thị danh sách tin nhắn của cuộc trò chuyện đang chọn.
const messageArea = el("messageArea");

// Form nhập và gửi tin nhắn.
const messageForm = el("messageForm");

// Ô nhập nội dung tin nhắn.
const messageInput = el("messageInput");

// Nút gửi tin nhắn trong form chat.
const sendButton = messageForm.querySelector(".send-button");

// Khung search user ở sidebar.
const searchBox = el("searchBox");

// Khu vực hiển thị kết quả tìm kiếm user.
const searchResult = el("searchResult");

// Ô nhập từ khóa tìm kiếm user.
const keywordInput = el("keyword");

// Nút thực hiện tìm kiếm user.
const searchButton = el("searchBtn");

// Nút chuyển theme sáng/tối của giao diện.
const themeToggle = document.querySelector(".theme-toggle");

// Modal hiển thị profile chi tiết của user.
const profileModal = el("profileModal");

// Vùng nội dung bên trong modal profile.
const profileModalContent = el("profileModalContent");

// Panel bên phải hiển thị thông tin nhóm hoặc thông tin user đang chat.
const groupInfo = el("groupInfo");

// Vùng nội dung của panel thông tin conversation.
const groupInfoContent = el("groupInfoContent");

// Nút mở panel thông tin conversation trên header chat.
const groupInfoButton = el("groupInfoButton");

// Modal hiển thị danh sách lời mời kết bạn.
const friendModal = el("friendModal");

// Danh sách lời mời kết bạn trong modal thông báo.
const friendRequestList = el("friendRequestList");

// Badge số lượng lời mời kết bạn mới trên icon chuông.
const friendRequestBadge = el("friendRequestBadge");

// Card tóm tắt danh sách bạn bè ở sidebar.
const friendListSummary = el("friendListSummary");

// Modal nhập lời nhắn trước khi gửi lời mời kết bạn.
const friendRequestMessageModal = el("friendRequestMessageModal");

// Form gửi lời nhắn kèm lời mời kết bạn.
const friendRequestMessageForm = el("friendRequestMessageForm");

// Ô nhập lời nhắn khi gửi kết bạn.
const friendRequestMessageInput = el("friendRequestMessageInput");

// Dòng mô tả người nhận trong modal gửi kết bạn.
const friendMessageSubtitle = el("friendMessageSubtitle");

// Modal tạo nhóm chat mới.
const groupCreateModal = el("groupCreateModal");

// Form tạo nhóm chat.
const groupCreateForm = el("groupCreateForm");

// Ô nhập tên nhóm chat.
const groupNameInput = el("groupNameInput");

// Khu vực render checkbox chọn thành viên khi tạo nhóm.
const groupMemberPicker = el("groupMemberPicker");

// Dòng lỗi validate tên nhóm, hiển thị dưới ô nhập tên nhóm.
const groupNameError = el("groupNameError");

// Dòng lỗi validate thành viên nhóm, hiển thị dưới danh sách chọn thành viên.
const groupMembersError = el("groupMembersError");

// Modal cài đặt tài khoản hiện tại.
const settingsModal = el("settingsModal");

// Form cập nhật thông tin /me.
const settingsForm = el("settingsForm");

// Ô nhập họ của user hiện tại.
const settingsFirstName = el("settingsFirstName");

// Ô nhập tên của user hiện tại.
const settingsLastName = el("settingsLastName");

// Ô nhập nickname của user hiện tại.
const settingsNickname = el("settingsNickname");

// Ô nhập số điện thoại của user hiện tại.
const settingsPhone = el("settingsPhone");

// Ô nhập URL avatar của user hiện tại.
const settingsAvatarUrl = el("settingsAvatarUrl");

// Ô nhập bio/giới thiệu ngắn của user hiện tại.
const settingsBio = el("settingsBio");

// Nút random màu logo/giao diện trong modal cài đặt.
const randomLogoColorBtn = el("randomLogoColorBtn");

// Danh sách endpoint frontend cần gọi tới backend.
// Các field dạng function dùng để gắn id/keyword an toàn bằng encodeURIComponent.
const API = {
    // Lấy danh sách conversation của user hiện tại.
    conversations: "/api/chat",

    // Lấy danh sách bạn bè.
    friends: "/api/friends",

    // Lấy danh sách lời mời kết bạn đã nhận/đã gửi.
    friendRequests: "/api/friends/requests",

    // Gửi lời mời kết bạn mới.
    sendFriendRequest: "/api/friends/requests",

    // Chấp nhận một lời mời kết bạn theo request id.
    acceptFriendRequest: id => `/api/friends/requests/${encodeURIComponent(id)}/accept`,

    // Từ chối một lời mời kết bạn theo request id.
    declineFriendRequest: id => `/api/friends/requests/${encodeURIComponent(id)}/decline`,

    // Tạo conversation mới, gồm direct chat hoặc group chat.
    createConversation: "/api/chat",

    // Thêm thành viên vào nhóm chat theo conversation id.
    addGroupMembers: id => `/api/chat/${encodeURIComponent(id)}/members`,

    // Lấy tin nhắn theo conversation id; cursor dùng để phân trang tin nhắn cũ.
    messages: (id, cursor = "") => {
        const url = `/api/chat/${encodeURIComponent(id)}/messages`;
        return cursor ? `${url}?cursor=${encodeURIComponent(cursor)}` : url;
    },

    // Gửi tin nhắn vào một conversation.
    sendMessage: id => `/api/chat/${encodeURIComponent(id)}/messages`,

    // Đánh dấu conversation đã xem/đã đọc.
    seen: id => `/api/chat/${encodeURIComponent(id)}/seen`,

    // Tìm kiếm user theo keyword.
    search: keyword => `/api/users/search?q=${encodeURIComponent(keyword)}`,

    // Lấy profile chi tiết của một user.
    profile: userId => `/api/users/${encodeURIComponent(userId)}/profile`,

    // Lấy thông tin tài khoản hiện tại.
    me: "/api/users/me",

    // Cập nhật thông tin tài khoản hiện tại.
    updateMe: "/api/users/me",

    // Cho phép backend/layout override endpoint nếu sau này cần đổi base path.
    ...(window.BunnyChatAPI || {})
};

// State tạm của màn Chat.
// Frontend dùng object này để lưu dữ liệu đã load từ server và trạng thái UI hiện tại.
const state = {
    // Thông tin user đang đăng nhập.
    currentUser: null,

    // Danh sách conversation lấy từ backend.
    conversations: [],

    // Danh sách bạn bè lấy từ backend.
    friends: [],

    // Danh sách lời mời kết bạn, tách thành lời mời nhận được và đã gửi đi.
    friendRequests: {
        received: [],
        sent: []
    },

    // Tab lời mời kết bạn đang mở trong modal: received hoặc sent.
    friendRequestTab: "received",

    // Conversation đang được chọn để xem/gửi tin nhắn.
    activeConversation: null,

    // User đang chờ gửi lời mời kết bạn kèm lời nhắn.
    pendingFriendRequest: null,

    // Kết nối SignalR hiện tại.
    connection: null,

    // Danh sách conversation id đã join vào SignalR group để tránh join lặp.
    joinedConversationIds: new Set(),

    // Danh sách message id đã render để tránh hiện trùng tin nhắn realtime.
    renderedMessageIds: new Set(),
    messageCache: new Map(),

    // Cursor trang tin nhắn cũ tiếp theo, dùng khi scroll lên để load thêm.
    messageNextCursor: null,

    // Cờ chống gọi load tin nhắn cũ nhiều lần cùng lúc.
    loadingOlderMessages: false
};

// Kiểm tra layout mobile để mở/đóng sidebar như một màn danh sách chat.
function isMobileChatLayout() {
    return window.matchMedia("(max-width: 720px)").matches;
}

// Mở sidebar danh sách chat trên giao diện mobile.
function openSidebar() {
    sidebar.classList.add("open");
}

// Đóng sidebar danh sách chat trên giao diện mobile.
function closeSidebar() {
    sidebar.classList.remove("open");
}

// Đồng bộ trạng thái sidebar khi đổi kích thước màn hình.
function syncMobileSidebar() {
    if (isMobileChatLayout() && !state.activeConversation) {
        openSidebar();
        return;
    }

    if (!isMobileChatLayout()) {
        closeSidebar();
    }
}

// Render thông tin tài khoản hiện tại ở footer sidebar.
function renderCurrentUser() {
    const user = state.currentUser;
    el("currentDisplayname").textContent = userDisplayName(user);
    el("currentUsername").textContent = `@${userName(user)}`;
    renderAvatarInto(el("currentAvatar"), user);
}

// Lấy dữ liệu chính từ ApiResponse của ASP.NET.
async function fetchData(url, options) {
    const response = await apiFetch(url, options);
    const result = await response.json().catch(() => ({}));

    if (!response.ok) {
        const validationMessage = result.errors
            ? Object.values(result.errors).flat().join("\n")
            : "";
        throw new Error(result.message || validationMessage || result.title || "Request failed");
    }

    return result.data ?? result;
}

// Gửi lời mời kết bạn từ card user trong kết quả tìm kiếm.
async function sendFriendRequestFromCard(user, button, messageText = "") {
    const oldText = button.textContent;
    button.disabled = true;
    button.textContent = "Đang gửi...";

    try {
        const request = await fetchData(API.sendFriendRequest, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                ReceiveId: itemId(user),
                Message: valueOr(messageText, "Mình muốn kết bạn với bạn.")
            })
        });

        state.friendRequests.sent.unshift({
            id: itemId(request),
            message: request.message,
            createdAt: request.createdAt,
            user
        });

        renderFriendRequestBadge();
        renderFriendRequests();
        button.textContent = "Đã gửi";
    } catch (error) {
        button.disabled = false;
        button.textContent = oldText;
        alert(error.message || "Không thể gửi lời mời kết bạn.");
    }
}

// Mở modal nhập lời nhắn trước khi gửi lời mời kết bạn.
function openFriendRequestMessageModal(user, button) {
    state.pendingFriendRequest = { user, button };
    friendMessageSubtitle.textContent = `Gửi lời mời đến ${conversationName(user)}`;
    friendRequestMessageInput.value = "Mình muốn kết bạn với bạn.";
    friendRequestMessageModal.classList.add("open");
    friendRequestMessageModal.setAttribute("aria-hidden", "false");
    friendRequestMessageInput.focus();
    friendRequestMessageInput.select();
}

// Đóng modal nhập lời nhắn kết bạn và xóa trạng thái tạm.
function closeFriendRequestMessageModal() {
    friendRequestMessageModal.classList.remove("open");
    friendRequestMessageModal.setAttribute("aria-hidden", "true");
    state.pendingFriendRequest = null;
}

// Gửi lời mời kết bạn kèm lời nhắn người dùng đã nhập.
async function submitFriendRequestMessage(event) {
    event.preventDefault();
    const pending = state.pendingFriendRequest;
    if (!pending) return;

    const messageText = friendRequestMessageInput.value.trim();
    closeFriendRequestMessageModal();
    await sendFriendRequestFromCard(pending.user, pending.button, messageText);
}

// Gọi API tạo direct conversation nếu bạn bè chưa có conversation.
async function getOrCreateDirectConversation(friend) {
    if (friend.conversation) {
        return {
            ...friend.conversation,
            directUserId: friend.conversation.directUserId || itemId(friend),
            userName: friend.conversation.userName || friend.userName || friend.username,
            username: friend.conversation.username || friend.username,
            displayname: friend.conversation.displayname || friend.displayname || friend.displayName
        };
    }

    const conversation = await fetchData(API.createConversation, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            type: "direct",
            memberIds: [itemId(friend)]
        })
    });

    const directConversation = {
        ...conversation,
        directUserId: conversation.directUserId || itemId(friend),
        userName: conversation.userName || friend.userName || friend.username,
        username: conversation.username || friend.username,
        displayname: conversation.displayname || friend.displayname || friend.displayName
    };

    upsertConversation(directConversation);
    return directConversation;
}

// Tạo card cuộc trò chuyện và gắn hành động theo từng loại card.
function createConversationCard(item, type) {
    const name = conversationName(item);
    const card = document.createElement("button");
    card.type = "button";
    card.className = "conversation-card";
    card.dataset.id = itemId(item.conversation || item);
    card.dataset.type = type;
    if (card.dataset.id && card.dataset.id === itemId(state.activeConversation)) {
        card.classList.add("active");
    }

    const info = document.createElement("span");
    info.className = "conversation-info";

    const title = document.createElement("strong");
    title.textContent = name;

    const preview = document.createElement("small");
    preview.textContent = item.lastMessage
        || item.conversation?.lastMessage
        || item.userName
        || item.username
        || (type === "group" ? `${item.memberCount || 0} thành viên` : "Chưa có tin nhắn");

    info.append(title, preview);
    card.append(createAvatar(item), info);

    if (type === "search") {
        card.classList.add("search-user-card");
        card.addEventListener("click", () => openUserProfile(itemId(item)));

        const action = document.createElement("span");
        action.className = "search-card-actions";

        const addButton = document.createElement("button");
        addButton.type = "button";
        addButton.className = "mini-action";

        const userId = itemId(item);
        const isMe = userId && userId === itemId(state.currentUser);

        if (isMe) {
            addButton.textContent = "Bạn";
            addButton.disabled = true;
        } else if (isFriend(state.friends, userId)) {
            addButton.textContent = "Đã là bạn";
            addButton.disabled = true;
        } else if (hasPendingRequest(state.friendRequests, userId)) {
            addButton.textContent = "Đã gửi/nhận";
            addButton.disabled = true;
        } else {
            addButton.textContent = "Kết bạn";
            addButton.addEventListener("click", event => {
                event.stopPropagation();
                openFriendRequestMessageModal(item, addButton);
            });
        }

        action.appendChild(addButton);
        card.appendChild(action);
    } else if (type === "friend") {
        card.addEventListener("click", async () => {
            setActiveCard(card);
            setEmpty(messageArea, "Đang mở cuộc trò chuyện...");
            const conversation = await getOrCreateDirectConversation(item);
            await selectConversation(conversation, card);
        });
    } else if (type !== "member") {
        card.addEventListener("click", () => selectConversation(item, card));
    }

    return card;
}

// Đánh dấu card đang được chọn.
function setActiveCard(card) {
    document.querySelector(".conversation-card.active")?.classList.remove("active");
    card?.classList.add("active");
}

// Render danh sách card vào container và cập nhật tổng số phần tử.
function renderList(container, count, items, type) {
    container.replaceChildren();
    count.textContent = String(items.length);

    if (!items.length) {
        setEmpty(container, type === "group" ? "Chưa có nhóm chat." : "Chưa có bạn bè.");
        return;
    }

    items.forEach(item => container.appendChild(createConversationCard(item, type)));
}

// Render danh sách nhóm chat từ dữ liệu server.
function renderGroups() {
    const groups = sortByLatestConversation(state.conversations.filter(item => item.type === "group"));
    renderList(groupList, el("groupCount"), groups, "group");
}

// Render danh sách bạn bè và ghép direct conversation nếu đã tồn tại.
function renderFriends() {
    const directConversations = state.conversations.filter(item => item.type === "direct");
    const friends = state.friends.map(friend => {
        const id = itemId(friend);
        const friendUserName = friend.userName || friend.username;
        const conversation = directConversations.find(item =>
            item.directUserId === id || (item.userName || item.username) === friendUserName
        );

        return {
            ...friend,
            conversation,
            lastMessage: conversation?.lastMessage
        };
    });
    const sortedFriends = sortByLatestConversation(friends);

    renderList(friendList, el("friendCount"), sortedFriends, "friend");
    renderFriendListSummary(sortedFriends);
}

// Render card tóm tắt danh sách bạn bè để sidebar nhìn rõ hơn.
function renderFriendListSummary(friends) {
    if (!friendListSummary) return;

    if (!friends.length) {
        friendListSummary.textContent = "Chưa có bạn bè. Hãy tìm user và gửi lời mời kết bạn.";
        return;
    }

    const previewNames = friends
        .slice(0, 3)
        .map(friend => conversationName(friend))
        .join(", ");

    friendListSummary.textContent = `${friends.length} bạn bè: ${previewNames}${friends.length > 3 ? "..." : ""}`;
}

// Tạo phần tử tin nhắn gửi hoặc nhận từ object tin nhắn.
function renderGroupMemberPicker() {
    groupMemberPicker.replaceChildren();

    if (!state.friends.length) {
        setEmpty(groupMemberPicker, "Cần có bạn bè trước khi tạo nhóm.");
        return;
    }

    state.friends.forEach(friend => {
        const id = itemId(friend);
        const label = document.createElement("label");
        label.className = "member-option";

        const checkbox = document.createElement("input");
        checkbox.type = "checkbox";
        checkbox.value = id;

        const body = document.createElement("span");
        const info = document.createElement("span");
        info.className = "member-option-info";

        const name = document.createElement("strong");
        name.textContent = conversationName(friend);

        const username = document.createElement("small");
        username.textContent = `@${friend.userName || friend.username || "unknown"}`;

        info.append(name, username);
        body.append(createAvatar(friend), info);
        label.append(checkbox, body);
        groupMemberPicker.appendChild(label);
    });
}

// Mở modal tạo nhóm và render danh sách bạn bè để chọn.
function openGroupCreateModal() {
    groupNameInput.value = "";
    clearGroupCreateErrors();
    renderGroupMemberPicker();
    groupCreateModal.classList.add("open");
    groupCreateModal.setAttribute("aria-hidden", "false");
    groupNameInput.focus();
}

// Đóng modal tạo nhóm chat.
function closeGroupCreateModal() {
    groupCreateModal.classList.remove("open");
    groupCreateModal.setAttribute("aria-hidden", "true");
}

// Xóa thông báo lỗi trong modal tạo nhóm.
function clearGroupCreateErrors() {
    groupNameError.textContent = "";
    groupMembersError.textContent = "";
    groupNameInput.classList.remove("input-error");
}

// Hiện lỗi inline dưới field tạo nhóm, không dùng alert trình duyệt.
function showGroupCreateError(field, message) {
    if (field === "name") {
        groupNameError.textContent = message;
        groupNameInput.classList.add("input-error");
        groupNameInput.focus();
        return;
    }

    groupMembersError.textContent = message;
}

// Gửi request tạo nhóm chat mới từ form tạo nhóm.
async function submitGroupCreate(event) {
    event.preventDefault();
    clearGroupCreateErrors();

    const name = groupNameInput.value.trim();
    const memberIds = Array
        .from(groupMemberPicker.querySelectorAll("input[type='checkbox']:checked"))
        .map(input => input.value)
        .filter(Boolean);

    if (!name) {
        showGroupCreateError("name", "Vui lòng nhập tên nhóm.");
        return;
    }

    if (!memberIds.length) {
        showGroupCreateError("members", "Vui lòng chọn ít nhất 1 thành viên.");
        return;
    }

    const submitButton = groupCreateForm.querySelector("button[type='submit']");
    submitButton.disabled = true;
    submitButton.textContent = "Đang tạo...";

    try {
        const conversation = await fetchData(API.createConversation, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                type: "group",
                name,
                memberIds
            })
        });

        closeGroupCreateModal();
        upsertConversation(conversation);
        renderGroups();
        renderFriends();
        await selectConversation(conversation);
    } catch (error) {
        showGroupCreateError("name", error.message || "Không thể tạo nhóm chat.");
    } finally {
        submitButton.disabled = false;
        submitButton.textContent = "Tạo nhóm";
    }
}

// Tạo phần tử UI cho một tin nhắn gồm avatar, tên, nội dung và thời gian.
const MESSENGER_EMOJIS = ["👍", "❤️", "😂", "😮", "😢", "😡"];

function renderReactionsBadge(container, reactions = [], currentUserId, messageId) {
    container.replaceChildren();
    if (!reactions || !reactions.length) {
        container.style.display = "none";
        return;
    }

    container.style.display = "inline-flex";
    const counts = {};
    let hasMyReaction = false;
    reactions.forEach(r => {
        if (!r.emoji) return;
        counts[r.emoji] = (counts[r.emoji] || 0) + 1;
        if (r.userId === currentUserId) hasMyReaction = true;
    });

    if (hasMyReaction) {
        container.classList.add("active-user-reaction");
    } else {
        container.classList.remove("active-user-reaction");
    }

    // Top emojis (up to 3 distinct)
    const distinctEmojis = Object.keys(counts);
    distinctEmojis.slice(0, 3).forEach(emoji => {
        const span = document.createElement("span");
        span.textContent = emoji;
        container.appendChild(span);
    });

    const total = reactions.length;
    if (total > 1) {
        const totalSpan = document.createElement("span");
        totalSpan.className = "reaction-item-count";
        totalSpan.textContent = total;
        container.appendChild(totalSpan);
    }
}

async function toggleMessageReaction(messageId, emoji) {
    try {
        const response = await apiFetch(`/api/chat/messages/${messageId}/reactions`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ emoji })
        });
        if (!response.ok) {
            console.error("Lỗi khi thả cảm xúc");
        }
    } catch (err) {
        console.error("Lỗi mạng khi thả cảm xúc", err);
    }
}

function createMessage(message) {
    const currentUserId = itemId(state.currentUser);
    const msgId = itemId(message);
    const senderId = message.senderId || message.SenderId || "";
    const isMine = senderId
        ? senderId === currentUserId
        : Boolean(message.isMine || message.isOwn);
    const sender = messageSender(message, state.currentUser, state.activeConversation);
    const row = document.createElement("article");
    row.className = `message ${isMine ? "outgoing" : "incoming"}`;
    row.title = sender.displayName;
    if (msgId) row.dataset.messageId = msgId;

    const body = document.createElement("span");
    body.className = "message-body";

    const senderName = document.createElement("strong");
    senderName.className = "message-sender";
    senderName.textContent = sender.displayName;

    // Bubble bọc p và reactions
    const bubbleWrapper = document.createElement("div");
    bubbleWrapper.className = "message-bubble-wrapper";

    const bubble = document.createElement("p");
    bubble.textContent = message.content || message.text || "";

    // Nút trigger reaction
    const reactTriggerBtn = document.createElement("button");
    reactTriggerBtn.type = "button";
    reactTriggerBtn.className = "reaction-trigger-btn";
    reactTriggerBtn.title = "Bày tỏ cảm xúc";
    reactTriggerBtn.innerHTML = "😀";

    // Picker popup (hỗ trợ cả click lẫn hover như Messenger)
    let pickerPopup = null;
    function openReactionPicker() {
        if (pickerPopup) return;
        document.querySelectorAll(".reaction-picker-popup").forEach(p => p.remove());

        pickerPopup = document.createElement("div");
        pickerPopup.className = "reaction-picker-popup";
        MESSENGER_EMOJIS.forEach(emoji => {
            const btn = document.createElement("button");
            btn.type = "button";
            btn.className = "reaction-picker-emoji";
            btn.textContent = emoji;
            btn.title = emoji;
            btn.addEventListener("click", async (ev) => {
                ev.stopPropagation();
                pickerPopup?.remove();
                pickerPopup = null;
                if (msgId) await toggleMessageReaction(msgId, emoji);
            });
            pickerPopup.appendChild(btn);
        });

        bubbleWrapper.appendChild(pickerPopup);
    }

    reactTriggerBtn.addEventListener("mouseenter", openReactionPicker);
    reactTriggerBtn.addEventListener("click", (e) => {
        e.stopPropagation();
        if (pickerPopup) {
            pickerPopup.remove();
            pickerPopup = null;
        } else {
            openReactionPicker();
        }
    });

    let hideTimer = null;
    function scheduleClosePicker() {
        clearTimeout(hideTimer);
        hideTimer = setTimeout(() => {
            if (pickerPopup) {
                pickerPopup.remove();
                pickerPopup = null;
            }
        }, 300);
    }

    function cancelClosePicker() {
        clearTimeout(hideTimer);
    }

    bubbleWrapper.addEventListener("mouseenter", cancelClosePicker);
    bubbleWrapper.addEventListener("mouseleave", scheduleClosePicker);

    // Badge reactions
    const reactionsBadge = document.createElement("div");
    reactionsBadge.className = "message-reactions-badge";
    renderReactionsBadge(reactionsBadge, message.reactions || [], currentUserId, msgId);

    // Quick click badge to default like or show picker
    reactionsBadge.addEventListener("click", (e) => {
        e.stopPropagation();
        reactTriggerBtn.click();
    });

    bubbleWrapper.append(bubble, reactTriggerBtn, reactionsBadge);

    const time = document.createElement("time");
    time.textContent = message.createdAt
        ? new Date(message.createdAt).toLocaleTimeString("vi-VN", { hour: "2-digit", minute: "2-digit" })
        : "";

    body.append(senderName, bubbleWrapper, time);
    row.append(createMessageAvatar(sender), body);
    return row;
}

// Kiểm tra người dùng có đang ở gần cuối khung chat không để quyết định auto scroll.
function isNearMessageBottom() {
    return messageArea.scrollHeight - messageArea.scrollTop - messageArea.clientHeight < 140;
}

// Render toàn bộ tin nhắn của cuộc trò chuyện đang được chọn.
function renderMessages(messages = []) {
    state.renderedMessageIds.clear();
    const sortedMessages = sortMessagesByTime(messages);

    if (!sortedMessages.length) {
        setEmpty(messageArea, "Chưa có tin nhắn.");
        return;
    }

    // Tối ưu render hàng loạt bằng DocumentFragment (giảm tối đa reflow/repaint DOM)
    const fragment = document.createDocumentFragment();
    sortedMessages.forEach(message => {
        const id = itemId(message);
        if (id) state.renderedMessageIds.add(id);
        fragment.appendChild(createMessage(message));
    });

    messageArea.replaceChildren(fragment);
    messageArea.scrollTop = messageArea.scrollHeight;
}

// Chèn tin nhắn cũ vào đầu danh sách và giữ vị trí đang đọc hiện tại.
function prependOlderMessages(messages = []) {
    const sortedMessages = sortMessagesByTime(messages);
    if (!sortedMessages.length) return;

    const previousHeight = messageArea.scrollHeight;
    const fragment = document.createDocumentFragment();

    sortedMessages.forEach(message => {
        const id = itemId(message);
        if (id && state.renderedMessageIds.has(id)) return;
        if (id) state.renderedMessageIds.add(id);
        fragment.appendChild(createMessage(message));
    });

    if (!fragment.childNodes.length) return;

    messageArea.prepend(fragment);
    messageArea.scrollTop = messageArea.scrollHeight - previousHeight;
}

// Tải trang tin nhắn cũ hơn khi người dùng kéo lên đầu khung chat.
async function loadOlderMessages() {
    const conversationId = itemId(state.activeConversation);
    if (!conversationId || !state.messageNextCursor || state.loadingOlderMessages) return;
    if (messageArea.querySelector(".empty-state")) return;

    state.loadingOlderMessages = true;
    try {
        const data = await fetchData(API.messages(conversationId, state.messageNextCursor));
        const page = readMessagePage(data);
        state.messageNextCursor = page.nextCursor;
        prependOlderMessages(page.messages);
    } catch {
        // Không chặn việc đọc chat nếu trang tin nhắn cũ tải thất bại.
    } finally {
        state.loadingOlderMessages = false;
    }
}

// Thêm một tin nhắn realtime vào khung chat nếu đang mở đúng conversation.
function appendRealtimeMessage(message) {
    const activeId = itemId(state.activeConversation);
    if (!activeId || messageConversationId(message) !== activeId) return;

    const id = itemId(message);
    if (id && state.renderedMessageIds.has(id)) return;
    if (id) state.renderedMessageIds.add(id);

    const shouldScrollToBottom = isNearMessageBottom() || message.senderId === itemId(state.currentUser);
    messageArea.querySelector(".empty-state")?.remove();
    messageArea.appendChild(createMessage(message));
    if (shouldScrollToBottom) {
        messageArea.scrollTop = messageArea.scrollHeight;
    }

    markActiveConversationAsSeen();
}

// Cập nhật hoặc thêm conversation vào state.
// Cập nhật preview conversation từ tin nhắn mới để card chat được đẩy lên đầu.
function updateConversationPreviewFromMessage(message) {
    const conversationId = messageConversationId(message);
    if (!conversationId) return;

    const index = state.conversations.findIndex(item => itemId(item) === conversationId);
    if (index < 0) return;

    const messageTime = message.createdAt || new Date().toISOString();
    state.conversations[index] = {
        ...state.conversations[index],
        lastMessage: message.content || message.text || state.conversations[index].lastMessage,
        lastMessageAt: messageTime,
        updatedAt: messageTime
    };

    state.conversations = sortByLatestConversation(state.conversations);
    renderGroups();
    renderFriends();
}

// Cập nhật conversation có sẵn hoặc thêm conversation mới vào state.
function upsertConversation(conversation) {
    if (!conversation) return;

    const id = itemId(conversation);
    const index = state.conversations.findIndex(item => itemId(item) === id);

    if (index >= 0) {
        state.conversations[index] = { ...state.conversations[index], ...conversation };
    } else {
        state.conversations.unshift(conversation);
    }

    state.conversations = sortByLatestConversation(state.conversations);
    if (itemId(state.activeConversation) === id) {
        state.activeConversation = { ...state.activeConversation, ...conversation };
    }
    renderGroups();
    renderFriends();
    joinConversation(id);
}

// Chọn cuộc trò chuyện, cập nhật header và tải danh sách tin nhắn.
async function selectConversation(item, card) {
    state.activeConversation = item;
    setActiveCard(card);
    if (isMobileChatLayout()) {
        closeSidebar();
    }

    const name = conversationName(item);
    el("chatTitle").textContent = name;
    el("chatStatus").textContent = item.type === "group"
        ? `${item.memberCount || item.members?.length || 0} thành viên`
        : "Tin nhắn riêng";
    renderAvatarInto(el("chatAvatar"), item);

    if (searchInChatBtn) searchInChatBtn.disabled = false;
    groupInfoButton.disabled = item.type !== "group" && !directChatUserId(item, state.friends);
    groupInfoButton.setAttribute("aria-label", item.type === "group" ? "Thông tin nhóm" : "Xem profile");
    groupInfoButton.title = item.type === "group" ? "Thông tin nhóm" : "Xem profile";
    renderGroupInfo(item.type === "group" ? item : null);

    messageInput.disabled = false;
    sendButton.disabled = false;
    messageInput.placeholder = "Soạn tin nhắn...";
    state.messageNextCursor = null;
    state.loadingOlderMessages = false;

    const convId = itemId(item);
    joinConversation(convId);

    // Hiển thị tin nhắn từ cache ngay lập tức nếu đã từng mở (0ms delay)
    if (state.messageCache && state.messageCache.has(convId)) {
        renderMessages(state.messageCache.get(convId));
    } else {
        setEmpty(messageArea, "Đang tải tin nhắn...");
    }

    try {
        const data = await fetchData(API.messages(convId));
        const page = readMessagePage(data);
        state.messageNextCursor = page.nextCursor;
        if (state.messageCache) state.messageCache.set(convId, page.messages);
        if (itemId(state.activeConversation) === convId) {
            renderMessages(page.messages);
            markActiveConversationAsSeen();
        }
    } catch {
        if (!state.messageCache || !state.messageCache.has(convId)) {
            setEmpty(messageArea, "Chưa thể tải tin nhắn từ máy chủ.");
        }
    }
}

// Render thông tin nhóm gồm tên, ngày tạo và danh sách thành viên.
function renderGroupInfo(group) {
    groupInfoContent.replaceChildren();
    if (!group) return;

    const name = conversationName(group);
    const hero = document.createElement("div");
    hero.className = "group-info-hero";
    hero.append(createAvatar(name));

    const heroText = document.createElement("div");
    const title = document.createElement("h2");
    title.textContent = name;
    const count = document.createElement("p");
    count.textContent = `${group.memberCount || group.members?.length || 0} thành viên`;
    heroText.append(title, count);
    hero.append(heroText);

    const details = document.createElement("dl");
    const createdAt = group.createdAt
        ? new Date(group.createdAt).toLocaleDateString("vi-VN")
        : "Chưa cập nhật";

    [["Tên nhóm", name], ["Số thành viên", String(group.memberCount || group.members?.length || 0)], ["Ngày tạo", createdAt]]
        .forEach(([label, value]) => {
            const row = document.createElement("div");
            const dt = document.createElement("dt");
            const dd = document.createElement("dd");
            dt.textContent = label;
            dd.textContent = value;
            row.append(dt, dd);
            details.append(row);
        });

    const members = document.createElement("div");
    members.className = "group-members";
    const membersTitle = document.createElement("strong");
    membersTitle.textContent = "Thành viên";
    members.append(membersTitle);
    (group.members || []).forEach(member => members.append(createConversationCard(member, "member")));

    groupInfoContent.append(hero, details, members, createAddGroupMembersForm(group));
}

// Tạo form thêm bạn bè chưa thuộc nhóm vào group chat hiện tại.
function createAddGroupMembersForm(group) {
    const wrapper = document.createElement("form");
    wrapper.className = "group-add-members";

    const title = document.createElement("strong");
    title.textContent = "Thêm thành viên";

    const memberIds = groupMemberIds(group);
    const candidates = state.friends.filter(friend => !memberIds.has(itemId(friend)));

    const picker = document.createElement("div");
    picker.className = "member-picker compact-picker";

    const error = document.createElement("small");
    error.className = "field-error";

    if (!candidates.length) {
        const empty = document.createElement("p");
        empty.className = "empty-state compact-empty";
        empty.textContent = "Không còn bạn bè nào để thêm vào nhóm.";
        picker.appendChild(empty);
    } else {
        candidates.forEach(friend => {
            const label = document.createElement("label");
            label.className = "member-option";

            const checkbox = document.createElement("input");
            checkbox.type = "checkbox";
            checkbox.value = itemId(friend);

            const body = document.createElement("span");
            const info = document.createElement("span");
            info.className = "member-option-info";

            const name = document.createElement("strong");
            name.textContent = conversationName(friend);

            const username = document.createElement("small");
            username.textContent = `@${friend.userName || friend.username || "unknown"}`;

            info.append(name, username);
            body.append(createAvatar(friend), info);
            label.append(checkbox, body);
            picker.appendChild(label);
        });
    }

    const actions = document.createElement("div");
    actions.className = "modal-actions";

    const submit = document.createElement("button");
    submit.className = "friend-action";
    submit.type = "submit";
    submit.textContent = "Thêm thành viên";
    submit.disabled = !candidates.length;
    actions.appendChild(submit);

    wrapper.append(title, picker, error, actions);
    wrapper.addEventListener("submit", event => submitAddGroupMembers(event, group, error, submit));

    return wrapper;
}

// Gửi danh sách thành viên mới lên backend; backend sẽ bỏ/chặn user đã thuộc nhóm.
async function submitAddGroupMembers(event, group, error, submitButton) {
    event.preventDefault();
    error.textContent = "";

    const memberIds = Array
        .from(event.currentTarget.querySelectorAll("input[type='checkbox']:checked"))
        .map(input => input.value)
        .filter(Boolean);

    if (!memberIds.length) {
        error.textContent = "Vui lòng chọn ít nhất 1 thành viên.";
        return;
    }

    submitButton.disabled = true;
    submitButton.textContent = "Đang thêm...";

    try {
        const updatedConversation = await fetchData(API.addGroupMembers(itemId(group)), {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ memberIds })
        });

        upsertConversation(updatedConversation);
        renderGroupInfo(updatedConversation);

        el("chatStatus").textContent = `${updatedConversation.memberCount || updatedConversation.members?.length || 0} thành viên`;
    } catch (apiError) {
        error.textContent = apiError.message || "Không thể thêm thành viên vào nhóm.";
    } finally {
        submitButton.disabled = false;
        submitButton.textContent = "Thêm thành viên";
    }
}

// Mở drawer thông tin của nhóm đang được chọn.
function openGroupInfo() {
    if (!state.activeConversation || state.activeConversation.type !== "group") return;
    groupInfo.classList.add("open");
    groupInfo.setAttribute("aria-hidden", "false");
}

// Mở thông tin của conversation hiện tại: group thì mở drawer, direct thì mở profile user.
function openConversationInfo() {
    if (!state.activeConversation) return;

    if (state.activeConversation.type === "group") {
        openGroupInfo();
        return;
    }

    openUserProfile(directChatUserId(state.activeConversation, state.friends));
}

// Đóng drawer thông tin nhóm.
function closeGroupInfo() {
    groupInfo.classList.remove("open");
    groupInfo.setAttribute("aria-hidden", "true");
}

// Cập nhật số thông báo lời mời kết bạn trên chuông.
function renderFriendRequestBadge() {
    const count = state.friendRequests.received.length;

    if (!friendRequestBadge) return;

    friendRequestBadge.hidden = count === 0;
    friendRequestBadge.textContent = String(count);
}

// Tải lời mời kết bạn đã nhận và đã gửi từ backend.
async function loadFriendRequests() {
    try {
        const data = await fetchData(API.friendRequests);
        state.friendRequests.received = Array.isArray(data.received) ? data.received : [];
        state.friendRequests.sent = Array.isArray(data.sent) ? data.sent : [];
    } catch {
        state.friendRequests.received = [];
        state.friendRequests.sent = [];
    }

    renderFriendRequestBadge();
    renderFriendRequests();
}

// Mở modal thông báo lời mời kết bạn.
function openFriendModal() {
    friendModal.classList.add("open");
    friendModal.setAttribute("aria-hidden", "false");
    renderFriendRequests();
}

// Đóng modal thông báo lời mời kết bạn.
function closeFriendModal() {
    friendModal.classList.remove("open");
    friendModal.setAttribute("aria-hidden", "true");
}

// Đổi tab lời mời đã nhận / đã gửi.
function setFriendRequestTab(tab) {
    state.friendRequestTab = tab;

    document.querySelectorAll("[data-friend-tab]").forEach(button => {
        button.classList.toggle("active", button.dataset.friendTab === tab);
    });

    renderFriendRequests();
}

// Render danh sách lời mời kết bạn trong modal thông báo.
function renderFriendRequests() {
    if (!friendRequestList) return;

    const list = state.friendRequests[state.friendRequestTab] || [];
    friendRequestList.replaceChildren();

    if (!list.length) {
        setEmpty(
            friendRequestList,
            state.friendRequestTab === "received"
                ? "Chưa có lời mời kết bạn mới."
                : "Bạn chưa gửi lời mời nào."
        );
        return;
    }

    list.forEach(request => friendRequestList.appendChild(createFriendRequestCard(request, state.friendRequestTab)));
}

// Tạo card lời mời kết bạn trong modal chuông.
function createFriendRequestCard(request, tab) {
    const user = request.user || {};
    const card = document.createElement("article");
    card.className = "friend-request-card";

    const main = document.createElement("div");
    main.className = "friend-request-main";
    main.appendChild(createAvatar(user));

    const text = document.createElement("span");
    text.className = "friend-request-text";
    const name = document.createElement("strong");
    name.textContent = conversationName(user);
    const username = document.createElement("small");
    username.textContent = `@${user.userName || user.username || "unknown"}`;
    text.append(name, username);
    main.appendChild(text);
    card.appendChild(main);

    if (request.message) {
        const message = document.createElement("p");
        message.className = "friend-request-message";
        message.textContent = request.message;
        card.appendChild(message);
    }

    const actions = document.createElement("div");
    actions.className = "friend-request-actions";

    if (tab === "received") {
        const accept = document.createElement("button");
        accept.type = "button";
        accept.className = "friend-action";
        accept.textContent = "Chấp nhận";
        accept.addEventListener("click", () => acceptFriendRequest(itemId(request)));

        const decline = document.createElement("button");
        decline.type = "button";
        decline.className = "friend-action secondary";
        decline.textContent = "Từ chối";
        decline.addEventListener("click", () => declineFriendRequest(itemId(request)));

        actions.append(accept, decline);
    } else {
        const sent = document.createElement("small");
        sent.textContent = "Đang chờ phản hồi";
        actions.appendChild(sent);
    }

    card.appendChild(actions);
    return card;
}

// Chấp nhận lời mời kết bạn và cập nhật danh sách bạn bè.
async function acceptFriendRequest(requestId) {
    const data = await fetchData(API.acceptFriendRequest(requestId), { method: "POST" });

    state.friendRequests.received = state.friendRequests.received.filter(request => itemId(request) !== requestId);

    if (data.newFriend && !isFriend(state.friends, itemId(data.newFriend))) {
        state.friends.unshift(data.newFriend);
    }

    renderFriendRequestBadge();
    renderFriendRequests();
    renderFriends();
}

// Từ chối lời mời kết bạn.
async function declineFriendRequest(requestId) {
    await apiFetch(API.declineFriendRequest(requestId), { method: "POST" });
    state.friendRequests.received = state.friendRequests.received.filter(request => itemId(request) !== requestId);
    renderFriendRequestBadge();
    renderFriendRequests();
}

// Đánh dấu conversation đang mở là đã xem.
async function markActiveConversationAsSeen() {
    if (!state.activeConversation) return;

    try {
        await fetchData(API.seen(itemId(state.activeConversation)), { method: "PATCH" });
    } catch {
        // Không chặn UI nếu trạng thái đã xem cập nhật thất bại.
    }
}

// Kết nối SignalR bằng access token hiện tại.
async function connectRealtime() {
    if (state.connection || !window.signalR) return;

    state.connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub", {
            accessTokenFactory: () => localStorage.getItem("accessToken") || ""
        })
        .withAutomaticReconnect()
        .build();

    state.connection.on("new-conversation", conversation => {
        upsertConversation(conversation);
    });

    state.connection.on("new-message", payload => {
        const message = payload?.message;
        const conversation = payload?.conversation;

        if (conversation) upsertConversation(conversation);
        if (message) {
            updateConversationPreviewFromMessage(message);
            appendRealtimeMessage(message);
        }
    });

    state.connection.on("read-message", payload => {
        if (payload?.conversation) upsertConversation(payload.conversation);
    });

    state.connection.on("reaction-updated", payload => {
        if (!payload || !payload.messageId) return;
        const msgRow = messageArea.querySelector(`[data-message-id="${payload.messageId}"]`);
        if (msgRow) {
            const badge = msgRow.querySelector(".message-reactions-badge");
            if (badge) {
                const currentUserId = itemId(state.currentUser);
                renderReactionsBadge(badge, payload.reactions || [], currentUserId, payload.messageId);
            }
        }
    });

    state.connection.on("friend-request-created", request => {
        if (request && !state.friendRequests.received.some(item => itemId(item) === itemId(request))) {
            state.friendRequests.received.unshift(request);
            renderFriendRequestBadge();
            renderFriendRequests();
        }
    });

    state.connection.on("friend-request-accepted", payload => {
        state.friendRequests.sent = state.friendRequests.sent.filter(request => itemId(request) !== payload?.requestId);

        if (payload?.user && !isFriend(state.friends, itemId(payload.user))) {
            state.friends.unshift(payload.user);
        }

        renderFriendRequestBadge();
        renderFriendRequests();
        renderFriends();
    });

    state.connection.on("friend-request-declined", payload => {
        state.friendRequests.sent = state.friendRequests.sent.filter(request => itemId(request) !== payload?.requestId);
        renderFriendRequests();
    });

    state.connection.onreconnected(async () => {
        state.joinedConversationIds.clear();
        state.conversations.forEach(conversation => joinConversation(itemId(conversation)));
        if (state.activeConversation) {
            joinConversation(itemId(state.activeConversation));
        }
    });

    try {
        await state.connection.start();
        state.conversations.forEach(conversation => joinConversation(itemId(conversation)));
        if (state.activeConversation) {
            joinConversation(itemId(state.activeConversation));
        }
    } catch (err) {
        console.warn("SignalR connection start error:", err);
    }
}

// Join SignalR group của conversation để nhận realtime message.
function joinConversation(conversationId) {
    if (!conversationId || !state.connection) return;
    if (state.connection.state !== signalR.HubConnectionState.Connected) {
        // Neu chua connected, thu lai sau 500ms
        setTimeout(() => joinConversation(conversationId), 500);
        return;
    }
    if (state.joinedConversationIds.has(conversationId)) return;

    state.joinedConversationIds.add(conversationId);
    state.connection.invoke("JoinConversation", conversationId).catch(() => {
        state.joinedConversationIds.delete(conversationId);
    });
}

// Tải dữ liệu thật từ backend, kết hợp Local Cache để UI hiển thị tức thì 0ms (Stale-While-Revalidate)
async function loadChatData() {
    // 1. Phục hồi dữ liệu từ localStorage cache nếu có để không bị treo "Đang tải..."
    try {
        const cachedUser = JSON.parse(localStorage.getItem("bunny_cache_user") || "null");
        if (cachedUser) {
            state.currentUser = cachedUser;
            renderCurrentUser();
        }

        const cachedConversations = JSON.parse(localStorage.getItem("bunny_cache_convs") || "null");
        if (Array.isArray(cachedConversations) && cachedConversations.length > 0) {
            state.conversations = cachedConversations;
            renderGroups();
            renderFriends();
        }

        const cachedFriends = JSON.parse(localStorage.getItem("bunny_cache_friends") || "null");
        if (Array.isArray(cachedFriends) && cachedFriends.length > 0) {
            state.friends = cachedFriends;
            renderFriends();
        }
    } catch {
        // Bỏ qua lỗi parse cache
    }

    if (!state.conversations.length) {
        setEmpty(groupList, "Đang tải nhóm chat...");
    }
    if (!state.friends.length) {
        setEmpty(friendList, "Đang tải bạn bè...");
    }
    setEmpty(messageArea, "Chọn một cuộc trò chuyện để xem tin nhắn.");

    const [me, conversations, friends, friendRequests] = await Promise.allSettled([
        fetchData(API.me),
        fetchData(API.conversations),
        fetchData(API.friends),
        fetchData(API.friendRequests)
    ]);

    if (me.status === "fulfilled" && me.value) {
        state.currentUser = me.value;
        renderCurrentUser();
        try { localStorage.setItem("bunny_cache_user", JSON.stringify(me.value)); } catch {}
    }

    if (conversations.status === "fulfilled" && Array.isArray(conversations.value)) {
        state.conversations = conversations.value;
        try { localStorage.setItem("bunny_cache_convs", JSON.stringify(conversations.value)); } catch {}
    }

    if (friends.status === "fulfilled" && Array.isArray(friends.value)) {
        state.friends = friends.value;
        try { localStorage.setItem("bunny_cache_friends", JSON.stringify(friends.value)); } catch {}
    }

    if (friendRequests.status === "fulfilled") {
        state.friendRequests.received = Array.isArray(friendRequests.value.received) ? friendRequests.value.received : [];
        state.friendRequests.sent = Array.isArray(friendRequests.value.sent) ? friendRequests.value.sent : [];
    }

    renderGroups();
    renderFriends();
    renderFriendRequestBadge();
    renderFriendRequests();
    await connectRealtime();
}

messageArea.addEventListener("scroll", () => {
    if (messageArea.scrollTop <= 60) {
        loadOlderMessages();
    }
});

let isSubmittingMessage = false;

messageForm.addEventListener("submit", async event => {
    event.preventDefault();

    const content = messageInput.value.trim();
    if (!content || !state.activeConversation || isSubmittingMessage) return;

    isSubmittingMessage = true;
    messageInput.value = "";

    const activeConversationId = itemId(state.activeConversation);
    const tempId = "temp-" + Date.now();
    const optimisticMsg = {
        id: tempId,
        conversationId: activeConversationId,
        senderId: itemId(state.currentUser),
        content,
        createdAt: new Date().toISOString(),
        isMine: true
    };

    // Optimistic UI: Hiển thị ngay lập tức lên khung chat không cần chờ server response
    updateConversationPreviewFromMessage(optimisticMsg);
    appendRealtimeMessage(optimisticMsg);

    try {
        const message = await fetchData(API.sendMessage(activeConversationId), {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ content })
        });

        const normalizedMessage = {
            ...message,
            conversationId: messageConversationId(message) || activeConversationId
        };

        const realId = itemId(normalizedMessage);
        if (realId) {
            state.renderedMessageIds.add(realId);
        }

        const tempRow = messageArea.querySelector(`[data-message-id="${tempId}"]`);
        if (tempRow) tempRow.dataset.messageId = realId;

        updateConversationPreviewFromMessage(normalizedMessage);
    } catch {
        const tempRow = messageArea.querySelector(`[data-message-id="${tempId}"]`);
        tempRow?.remove();
        state.renderedMessageIds.delete(tempId);
        messageInput.value = content;
    } finally {
        isSubmittingMessage = false;
    }
});

// Đóng khung tìm kiếm và xóa trạng thái tìm kiếm hiện tại.
function closeSearch() {
    searchBox.classList.remove("open");
    searchResult.replaceChildren();
    keywordInput.value = "";
    sidebar.classList.remove("search-active");
}

// Hiển thị thông báo trạng thái trong danh sách kết quả tìm kiếm.
function showSearchState(text) {
    sidebar.classList.add("search-active");
    setEmpty(searchResult, text);
}

// Tìm kiếm người dùng theo tên hoặc username từ API.
async function searchUsers() {
    const keyword = keywordInput.value.trim();
    if (!keyword) return showSearchState("Nhập tên hoặc username để tìm kiếm.");

    showSearchState("Đang tìm người dùng...");

    try {
        const users = await fetchData(API.search(keyword));
        searchResult.replaceChildren();
        users.forEach(user => searchResult.appendChild(createConversationCard(user, "search")));
        if (!users.length) showSearchState("Không tìm thấy người dùng.");
    } catch {
        showSearchState("Không tìm thấy người dùng.");
    }
}

// Đóng modal thông tin người dùng.
function closeUserProfile() {
    profileModal.classList.remove("open");
    profileModal.setAttribute("aria-hidden", "true");
}

// Tải và hiển thị thông tin chi tiết của người dùng được chọn.
function openSettingsModal() {
    const user = state.currentUser || {};
    settingsFirstName.value = userField(user, "firstName");
    settingsLastName.value = userField(user, "lastName");
    settingsNickname.value = userField(user, "nickname");
    settingsPhone.value = userField(user, "phone");
    settingsAvatarUrl.value = avatarUrlOf(user);
    settingsBio.value = userField(user, "bio");

    settingsModal.classList.add("open");
    settingsModal.setAttribute("aria-hidden", "false");
    settingsFirstName.focus();
}

// Đóng modal cài đặt tài khoản.
function closeSettingsModal() {
    settingsModal.classList.remove("open");
    settingsModal.setAttribute("aria-hidden", "true");
}

// Gửi request cập nhật thông tin /me từ form cài đặt.
async function submitSettings(event) {
    event.preventDefault();

    const submitButton = settingsForm.querySelector("button[type='submit']");
    submitButton.disabled = true;
    submitButton.textContent = "Đang lưu...";

    try {
        const updatedUser = await fetchData(API.updateMe, {
            method: "PATCH",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                firstName: settingsFirstName.value.trim() || null,
                lastName: settingsLastName.value.trim() || null,
                nickname: settingsNickname.value.trim() || null,
                phone: settingsPhone.value.trim() || null,
                avatarUrl: settingsAvatarUrl.value.trim() || null,
                bio: settingsBio.value.trim() || null
            })
        });

        state.currentUser = { ...state.currentUser, ...updatedUser };
        renderCurrentUser();
        closeSettingsModal();
    } catch (error) {
        alert(error.message || "Không thể cập nhật thông tin.");
    } finally {
        submitButton.disabled = false;
        submitButton.textContent = "Lưu thay đổi";
    }
}

// Mở modal hồ sơ và tải thông tin chi tiết của user.
async function openUserProfile(userId) {
    if (!userId) return;

    profileModal.classList.add("open");
    profileModal.setAttribute("aria-hidden", "false");
    profileModalContent.textContent = "Đang tải hồ sơ...";

    try {
        const user = await fetchData(API.profile(userId));
        const hero = document.createElement("div");
        hero.className = "profile-hero";
        hero.appendChild(createAvatar(user));

        const heroText = document.createElement("span");
        const heading = document.createElement("h2");
        heading.id = "profileModalName";
        heading.textContent = valueOr(user.displayname || user.displayName, "Người dùng");
        const usernameText = document.createElement("small");
        usernameText.textContent = `@${user.userName || user.username || "unknown"}`;
        heroText.append(heading, usernameText);
        hero.appendChild(heroText);

        const list = document.createElement("dl");
        [["Tên", user.displayname || user.displayName], ["Username", user.userName || user.username], ["Số điện thoại", user.phone], ["Nickname", user.nickname], ["Bio", user.bio]]
            .forEach(([label, value]) => {
                const row = document.createElement("div");
                const dt = document.createElement("dt");
                const dd = document.createElement("dd");
                dt.textContent = label;
                dd.textContent = valueOr(value);
                row.append(dt, dd);
                list.appendChild(row);
            });

        profileModalContent.replaceChildren(hero, list);
    } catch {
        profileModalContent.textContent = "Không thể tải thông tin người dùng.";
    }
}

// Chuyển theme sáng/tối và lưu lựa chọn vào localStorage.
const logoPalettes = [
    ["#9333ea", "#d946ef"],
    ["#f97316", "#fb7185"],
    ["#06b6d4", "#3b82f6"],
    ["#10b981", "#84cc16"],
    ["#ec4899", "#8b5cf6"],
    ["#f59e0b", "#ef4444"]
];

// Áp dụng màu logo/giao diện và lưu lựa chọn vào localStorage.
function applyLogoColor(accent, accent2) {
    document.documentElement.style.setProperty("--accent", accent);
    document.documentElement.style.setProperty("--accent-2", accent2);
    localStorage.setItem("bunny-logo-colors", JSON.stringify([accent, accent2]));
}

// Chọn ngẫu nhiên một bộ màu logo từ danh sách có sẵn.
function randomLogoColor() {
    const [accent, accent2] = logoPalettes[Math.floor(Math.random() * logoPalettes.length)];
    applyLogoColor(accent, accent2);
}

// Chuyển theme sáng/tối và lưu lựa chọn vào localStorage.
function setTheme(theme) {
    document.documentElement.dataset.theme = theme;
    localStorage.setItem("bunny-theme", theme);
    themeToggle.classList.toggle("on", theme === "dark");
}

const openSearchBtn = el("openSearchBtn");
if (openSearchBtn) {
    openSearchBtn.addEventListener("click", () => {
        if (searchBox.classList.contains("open")) {
            closeSearch();
            return;
        }

        searchBox.classList.add("open");
        keywordInput.focus();
    });
}

el("searchDismiss").addEventListener("click", closeSearch);
searchButton.addEventListener("click", searchUsers);
keywordInput.addEventListener("keydown", event => {
    if (event.key === "Enter") searchUsers();
});
themeToggle.addEventListener("click", () => setTheme(document.documentElement.dataset.theme === "dark" ? "light" : "dark"));
el("sidebarToggle").addEventListener("click", () => sidebar.classList.toggle("open"));
closeSidebarButton.addEventListener("click", closeSidebar);
window.addEventListener("resize", syncMobileSidebar);
groupInfoButton.addEventListener("click", openConversationInfo);
el("openFriendRequestsBtn").addEventListener("click", openFriendModal);
el("openGroupCreateBtn").addEventListener("click", openGroupCreateModal);
el("openSettingsBtn").addEventListener("click", openSettingsModal);
friendRequestMessageForm.addEventListener("submit", submitFriendRequestMessage);
groupCreateForm.addEventListener("submit", submitGroupCreate);
groupNameInput.addEventListener("input", () => {
    groupNameError.textContent = "";
    groupNameInput.classList.remove("input-error");
});
groupMemberPicker.addEventListener("change", () => {
    groupMembersError.textContent = "";
});
settingsForm.addEventListener("submit", submitSettings);
randomLogoColorBtn.addEventListener("click", randomLogoColor);
document.querySelectorAll("[data-close-group-info]").forEach(button => button.addEventListener("click", closeGroupInfo));
document.querySelectorAll("[data-close-profile]").forEach(button => button.addEventListener("click", closeUserProfile));
document.querySelectorAll("[data-close-friend-modal]").forEach(button => button.addEventListener("click", closeFriendModal));
document.querySelectorAll("[data-close-friend-message]").forEach(button => button.addEventListener("click", closeFriendRequestMessageModal));
document.querySelectorAll("[data-close-group-create]").forEach(button => button.addEventListener("click", closeGroupCreateModal));
document.querySelectorAll("[data-close-settings]").forEach(button => button.addEventListener("click", closeSettingsModal));
document.querySelectorAll("[data-friend-tab]").forEach(button => {
    button.addEventListener("click", () => setFriendRequestTab(button.dataset.friendTab));
});
const logoutBtn = el("logoutBtn");
if (logoutBtn) {
    logoutBtn.addEventListener("click", async event => {
        event.preventDefault();
        // 1. Xóa ngay token trên client để chặn mọi request tiếp theo
        localStorage.removeItem("accessToken");
        sessionStorage.clear();
        try {
            // 2. Dùng sendBeacon hoặc fetch no-cors/direct để xóa refresh cookie trên backend mà không bị chặn
            await fetch("/api/auth/signout", {
                method: "POST",
                credentials: "include"
            });
        } catch (err) {
            console.warn("Signout request error:", err);
        } finally {
            // 3. Chuyển hướng ngay lập tức về trang đăng nhập
            window.location.href = "/";
        }
    });
}


// ==================== TÌM KIẾM TIN NHẮN TRONG ĐOẠN CHAT & TOÀN BỘ ====================
const searchInChatBtn = el("searchInChatBtn");
const searchAllChatsBtn = el("searchAllChatsBtn");
const inChatSearchBar = el("inChatSearchBar");
const inChatSearchInput = el("inChatSearchInput");
const closeInChatSearchBtn = el("closeInChatSearchBtn");

const searchAllModal = el("searchAllModal");
const searchAllInput = el("searchAllInput");
const searchAllSubmitBtn = el("searchAllSubmitBtn");
const searchAllResults = el("searchAllResults");

// Đóng mở bảng reaction khi click bên ngoài
document.addEventListener("click", () => {
    document.querySelectorAll(".reaction-picker-popup").forEach(p => p.remove());
});

// 1. Tìm kiếm trong đoạn chat đang mở
if (searchInChatBtn) {
    searchInChatBtn.addEventListener("click", () => {
        if (!state.activeConversation) return;
        const isHidden = inChatSearchBar.hidden;
        inChatSearchBar.hidden = !isHidden;
        if (!inChatSearchBar.hidden) {
            inChatSearchInput.value = "";
            inChatSearchInput.focus();
        } else {
            resetInChatSearch();
        }
    });
}

if (closeInChatSearchBtn) {
    closeInChatSearchBtn.addEventListener("click", () => {
        inChatSearchBar.hidden = true;
        resetInChatSearch();
    });
}

function resetInChatSearch() {
    inChatSearchInput.value = "";
    messageArea.querySelectorAll(".message").forEach(row => {
        row.classList.remove("search-hidden", "search-match");
    });
}

if (inChatSearchInput) {
    inChatSearchInput.addEventListener("input", () => {
        const keyword = inChatSearchInput.value.trim().toLowerCase();
        const rows = messageArea.querySelectorAll(".message");
        if (!keyword) {
            rows.forEach(r => r.classList.remove("search-hidden", "search-match"));
            return;
        }

        let firstMatch = null;
        rows.forEach(r => {
            const text = (r.querySelector("p")?.textContent || "").toLowerCase();
            if (text.includes(keyword)) {
                r.classList.remove("search-hidden");
                r.classList.add("search-match");
                if (!firstMatch) firstMatch = r;
            } else {
                r.classList.add("search-hidden");
                r.classList.remove("search-match");
            }
        });

        if (firstMatch) {
            firstMatch.scrollIntoView({ behavior: "smooth", block: "center" });
        }
    });
}

// 2. Tìm kiếm trong toàn bộ đoạn chat
function openSearchAllModal() {
    searchAllModal.classList.add("open");
    searchAllModal.setAttribute("aria-hidden", "false");
    searchAllInput.value = "";
    searchAllResults.innerHTML = '<p class="search-empty-hint">Nhập từ khóa và nhấn tìm kiếm</p>';
    setTimeout(() => searchAllInput.focus(), 80);
}

function closeSearchAllModal() {
    searchAllModal.classList.remove("open");
    searchAllModal.setAttribute("aria-hidden", "true");
}

if (searchAllChatsBtn) {
    searchAllChatsBtn.addEventListener("click", openSearchAllModal);
}

document.querySelectorAll("[data-close-search-all]").forEach(btn => {
    btn.addEventListener("click", closeSearchAllModal);
});

async function executeSearchAll() {
    const query = searchAllInput.value.trim();
    if (!query) {
        searchAllResults.innerHTML = '<p class="search-empty-hint">Vui lòng nhập từ khóa tìm kiếm</p>';
        return;
    }

    searchAllResults.innerHTML = '<p class="search-empty-hint">Đang tìm kiếm tin nhắn...</p>';

    try {
        const response = await apiFetch(`/api/chat/search-messages?query=${encodeURIComponent(query)}`);
        const result = await response.json();

        if (!response.ok || !result.success) {
            searchAllResults.innerHTML = `<p class="search-empty-hint">${result.message || "Lỗi tìm kiếm"}</p>`;
            return;
        }

        const items = result.data || [];
        if (!items.length) {
            searchAllResults.innerHTML = `<p class="search-empty-hint">Không tìm thấy tin nhắn nào chứa "${query}"</p>`;
            return;
        }

        searchAllResults.replaceChildren();
        items.forEach(({ message, conversation }) => {
            const card = document.createElement("div");
            card.className = "search-result-item";

            const convName = conversation?.name || "Cuộc trò chuyện";
            const senderName = message?.senderName || "Người dùng";
            const timeStr = message?.createdAt ? new Date(message.createdAt).toLocaleString("vi-VN") : "";
            const rawContent = message?.content || "";

            // Highlight từ khóa
            const safeQuery = query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
            const highlighted = rawContent.replace(new RegExp(`(${safeQuery})`, "gi"), '<mark class="search-highlight">$1</mark>');

            card.innerHTML = `
                <div class="search-result-header">
                    <strong>${convName} · ${senderName}</strong>
                    <time>${timeStr}</time>
                </div>
                <div class="search-result-content">${highlighted}</div>
            `;

            card.addEventListener("click", async () => {
                closeSearchAllModal();
                if (conversation) {
                    await selectConversation(conversation);
                    // Sau khi tải tin nhắn, scroll tới tin nhắn này nếu có
                    setTimeout(() => {
                        const targetRow = messageArea.querySelector(`[data-message-id="${message.id}"]`);
                        if (targetRow) {
                            targetRow.classList.add("search-match");
                            targetRow.scrollIntoView({ behavior: "smooth", block: "center" });
                            setTimeout(() => targetRow.classList.remove("search-match"), 3000);
                        }
                    }, 400);
                }
            });

            searchAllResults.appendChild(card);
        });
    } catch (err) {
        searchAllResults.innerHTML = '<p class="search-empty-hint">Không thể kết nối đến máy chủ.</p>';
    }
}

if (searchAllSubmitBtn) {
    searchAllSubmitBtn.addEventListener("click", executeSearchAll);
}

if (searchAllInput) {
    searchAllInput.addEventListener("keydown", (e) => {
        if (e.key === "Enter") {
            e.preventDefault();
            executeSearchAll();
        }
    });
}

window.BunnyChatUI = {
    state,
    renderGroups,
    renderFriends,
    renderMessages,
    renderGroupInfo,
    selectConversation,
    loadChatData
};

try {
    const savedLogoColors = JSON.parse(localStorage.getItem("bunny-logo-colors") || "null");
    if (Array.isArray(savedLogoColors) && savedLogoColors.length === 2) {
        applyLogoColor(savedLogoColors[0], savedLogoColors[1]);
    }
} catch {
    localStorage.removeItem("bunny-logo-colors");
}

setTheme(document.documentElement.dataset.theme || "dark");
syncMobileSidebar();
loadChatData();
